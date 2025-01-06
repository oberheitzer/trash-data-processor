using System.Globalization;
using System.IO.Abstractions;
using CsvHelper;
using CsvHelper.Configuration;
using TDP.Domain.Enum;
using TDP.Domain.Model;
using TDP.Extractor.Helpers;
using TDP.Extractor.Interfaces;
using TDP.Extractor.Mappers;
using UglyToad.PdfPig;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;

namespace TDP.Extractor.Services;

internal sealed class CalendarService : ICalendarService
{
    private int Id = 1;
    private readonly int OneDay = 1;
    private readonly int SevenDays = 7;

    private readonly IFileSystem _fileSystem;

    public CalendarService(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public IEnumerable<List<Collection>> Read(List<Domain.Model.Calendar> calendars)
    {
        foreach (Domain.Model.Calendar calendar in calendars)
        {
            string path = $"{_fileSystem.GetDirectoryPath(Shared.Constants.File.Calendars)}/{calendar.Name}.pdf";
            if (_fileSystem.File.Exists(path: path))
            {
                List<Collection> collections = [];
                ReadLines(lines: GetLines(path: path), collections: collections, calendarId: calendar.Id);
                yield return collections;
            }
        }
    }

    public void Write(List<Collection> collections)
    {
        string file = $"{_fileSystem.GetDirectoryPath(folderName: Shared.Constants.File.Data)}/{Shared.Constants.File.Collections}";
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            // Don't write the header again.
            HasHeaderRecord = !_fileSystem.File.Exists(path: file),
        };
        using StreamWriter writer = _fileSystem.File.AppendText(path: file);
        using CsvWriter csv = new(writer: writer, configuration: config);
        csv.Context.RegisterClassMap<CollectionMapper>();
        csv.WriteRecords(records: collections);
    }

    /// <summary>
    /// Inserts the dates of a specific day in the whole year.
    /// </summary>
    /// <param name="dayOfWeek">Day of week when the collection happens.</param>
    /// <param name="rescheduledDays">Rescheduled days.</param>
    /// <param name="collections">Collections days.</param>
    /// <param name="waste">The type of waste.</param>
    /// <param name="calendarId">Unique identifier of Calendar.</param>
    private void AddCollection(
        DayOfWeek dayOfWeek,
        Dictionary<DateOnly, DateOnly> rescheduledDays,
        List<Collection> collections,
        Waste waste,
        int calendarId)
    {
        var firstDay = new DateOnly(year: 2025, month: 1, day: 1);
        var lastDay = new DateOnly(year: 2025, month: 12, day: 31);

        DateOnly current = firstDay;

        while (current.DayOfWeek != dayOfWeek)
        {
            current = current.AddDays(value: OneDay);
        }

        while (current <= lastDay)
        {
            var collection = Converter.ToCollection(id: Id++, calendarId: calendarId, waste: waste);
            if (rescheduledDays.TryGetValue(current, out DateOnly value))
            {
                collection.Date = value;
            }
            else
            {
                collection.Date = current;
            }
            collections.Add(collection);
            current = current.AddDays(value: SevenDays);
        }
    }

    /// <summary>
    /// Extracts and inserts the days of collection.
    /// </summary>
    /// <param name="data">The days.</param>
    /// <param name="collections">Collection days.</param>
    /// <param name="waste">The type of the waste.</param>
    /// <param name="calendarId">Unique identifier of Calendar.</param>
    private void AddCollection(
        string data,
        List<Collection> collections,
        Waste waste,
        int calendarId)
    {
        string formattedData = data.Replace(oldValue: Separator.CommaWithSpace, newValue: Separator.Comma.ToString());
        string[] months = formattedData.Split(separator: Separator.Space);
        int monthIndex = 1;
        foreach (string month in months)
        {
            string[] days = month.Split(separator: Separator.Comma);
            for (int i = 0; i < days.Length; i++)
            {
                if (int.TryParse(days[i], out int day))
                {
                    collections.Add(Converter.ToCollection(
                        id: Id++,
                        calendarId: calendarId,
                        monthIndex: monthIndex,
                        day: day,
                        waste: waste));
                }
            }
            monthIndex++;
        }
    }

    /// <summary>
    /// Inserts a date pair into collection which stores the rescheduled days.
    /// </summary>
    /// <param name="rescheduledDays">Key-value collections which holds the rescheduled dates.</param>
    /// <param name="dates">The old and the new value.</param>
    private static void AddDate(Dictionary<DateOnly, DateOnly> rescheduledDays, string[] dates)
    {
        string oldDate = dates[0].Substring(startIndex: 0, length: dates[0].IndexOf(value: Separator.Dot));
        string newDate = dates[1].Trim().Substring(startIndex: 0, length: dates[1].Trim().IndexOf(value: Separator.Dot));

        string[] oldMonthAndDay = oldDate.Split(separator: Separator.Space);
        string[] newMonthAndDay = newDate.Split(separator: Separator.Space);

        rescheduledDays.Add(
            key: Converter.ToDate(month: oldMonthAndDay[0], day: oldMonthAndDay[1]),
            value: Converter.ToDate(month: newMonthAndDay[0], day: newMonthAndDay[1]));
    }

    /// <summary>
    /// Returns the waste collection days.
    /// There are some cases when the days are not in the same line.
    /// </summary>
    /// <param name="line">The current line.</param>
    /// <param name="lines">All of the lines in the PDF files.</param>
    /// <param name="index">Current iteration.</param>
    /// <returns></returns>
    private static string ExtractDays(string line, string[] lines, int index)
    {
        if (line.Length == Text.RecyclableWaste.Length)
        {
            return lines[index + 3] + Separator.Space + lines[index + 4];
        }
        else if (DateOnly.TryParse(line.Split(separator: Separator.Space)[1], out var _))
        {
            return lines[index + 1];
        }
        return line.Substring(startIndex: Text.RecyclableWaste.Length).Trim();
    }

    /// <summary>
    /// Read the content of the PDF file and returns with the lines.
    /// </summary>
    /// <param name="name">Name of the file.</param>
    /// <returns>Lines.</returns>
    private string[] GetLines(string path)
    {
        using var document = PdfDocument.Open(_fileSystem.File.Open(path, FileMode.Open));
        string text = ContentOrderTextExtractor.GetText(page: document.GetPage(pageNumber: 1));
        return text.Split([Environment.NewLine], StringSplitOptions.RemoveEmptyEntries);
    }

    /// <summary>
    /// Read lines.
    /// </summary>
    /// <param name="lines">Content of the PDF file.</param>
    /// <param name="collections">Collection days.</param>
    /// <param name="calendarId">Unique identifier of the calendar.</param>
    private void ReadLines(string[] lines, List<Collection> collections, int calendarId)
    {
        bool hasRescheduledDays = false;
        var rescheduledDays = new Dictionary<DateOnly, DateOnly>();
        DayOfWeek? dayOfWeek = null;

        for (int index = 0; index < lines.Length; index++)
        {
            string line = lines[index];
            if (line.Contains(Text.SolidWaste))
            {
                string[] lineParts = line.Split(separator: Separator.Colon);
                string day = lineParts[1].Trim();
                dayOfWeek = Converter.ToDayOfWeek(day: day);
            }

            if (line.Contains(Text.RescheduledDays))
            {
                string[] lineParts = line.Split(separator: Separator.Colon);
                string[] dates = lineParts[1].Trim().Split(separator: Text.InsteadOf);

                AddDate(rescheduledDays: rescheduledDays, dates: dates);

                hasRescheduledDays = true;
            }

            if (!hasRescheduledDays && line.Contains(Text.InsteadOf))
            {
                AddDate(rescheduledDays: rescheduledDays, dates: line.Trim().Split(separator: Text.InsteadOf));
            }

            if (line.StartsWith(Text.RecyclableWaste))
            {
                AddCollection(
                    data: ExtractDays(line, lines, index),
                    collections: collections,
                    waste: Waste.Recyclable,
                    calendarId: calendarId);
            }

            if (line.StartsWith(Text.OrganicWaste) && int.TryParse(line[Text.OrganicWaste.Length + 1].ToString(), out int _))
            {
                AddCollection(
                    data: line.Substring(startIndex: Text.OrganicWaste.Length).Trim(),
                    collections: collections,
                    waste: Waste.Organic,
                    calendarId: calendarId);
            }
        }

        if (dayOfWeek != null)
        {
            AddCollection(
                dayOfWeek: dayOfWeek.Value,
                rescheduledDays: rescheduledDays,
                collections: collections,
                waste: Waste.Solid,
                calendarId: calendarId);
        }
    }
}
