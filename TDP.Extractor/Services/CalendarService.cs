using System.Globalization;
using System.IO.Abstractions;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using TDP.Domain.Model;
using TDP.Extractor.Helpers;
using TDP.Extractor.Interfaces;
using TDP.Extractor.Mappers;
using UglyToad.PdfPig;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;

namespace TDP.Extractor.Services;

internal sealed class CalendarService : ICalendarService
{
    private int _id = 1;

    private readonly IFileSystem _fileSystem;

    public CalendarService(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public IEnumerable<List<Collection>> Read(List<Domain.Model.Calendar> calendars)
    {
        foreach (Domain.Model.Calendar info in calendars)
        {
            // (string calendar, int year, int areaId) = Extract(lines: GetLines(name: info.Name), areas: areas);

            // string[] dayLines = calendar.Split([Environment.NewLine], StringSplitOptions.RemoveEmptyEntries);
            List<Collection> collections = [];
            // int dayIndex = 1;

            // foreach (string dayLine in dayLines)
            // {
            //     ReadLine(
            //         collections: collections,
            //         line: dayLine,
            //         day: dayIndex,
            //         year: year,
            //         areaId: areaId
            //     );

            //     dayIndex++;
            // }

            yield return collections;
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
    /// Extract the necessary data from the downloaded PDF file.
    /// </summary>
    /// <param name="lines">All lines from the file.</param>
    /// <returns>The calendar lines, the year and the type of the property of waste collection.</returns>
    // private static (string calendar, int year, int areaId) Extract(string[] lines, List<Area> areas)
    // {
    //     StringBuilder sb = new();
    //     int areaId = 0;
    //     int currentYear = DateTime.Now.Year;
    //     foreach (string line in lines)
    //     {
    //         string dayString = line.Split(' ')[0];
    //         bool isNumber = int.TryParse(dayString, out int day);
    //         if (isNumber && day >= Constant.FirstDayOfMonth && day <= Constant.LastDayOfLongerMonth)
    //         {
    //             sb.AppendLine(line);
    //         }

    //         if (line.Contains(Constant.WasteCalendar))
    //         {
    //             areaId = Converter.ToAreaId(area: Converter.ToArea(line: line), areas: areas);
    //             bool isYear = int.TryParse(line.Substring(startIndex: 0, length: line.IndexOf('.')), out int year);
    //             if (isYear && year >= currentYear)
    //             {
    //                 currentYear = year;
    //             }
    //         }
    //     }
    //     return (calendar: sb.ToString(), year: currentYear, areaId);
    // }

    /// <summary>
    /// Read the content of the PDF file and returns with the lines.
    /// </summary>
    /// <param name="name">Name of the file.</param>
    /// <returns>Lines.</returns>
    private string[] GetLines(string name)
    {
        string path = $"{_fileSystem.GetDirectoryPath(Shared.Constants.File.Calendars)}/{name}.pdf";
        using var document = PdfDocument.Open(_fileSystem.File.Open(path, FileMode.Open));
        string text = ContentOrderTextExtractor.GetText(page: document.GetPage(pageNumber: 1));
        return text.Split([Environment.NewLine], StringSplitOptions.RemoveEmptyEntries);
    }

    /// <summary>
    /// Increases the value of a month based on its days' amount.
    /// </summary>
    /// <param name="month">Previous month.</param>
    /// <param name="day">Day of the month.</param>
    /// <param name="index">One of the days when waste collection happens.</param>
    /// <param name="year">Current year.</param>
    private static void Increase(ref int month, int day, int index, int year)
    {
        if ((index == Constant.IndexOfJanuary && (day == Constant.LastDayOfShorterMonth || (!DateTime.IsLeapYear(year) && day == Constant.LastDayOfShortestMonthInLeapYear))) ||
            (day == Constant.LastDayOfLongerMonth && index != Constant.IndexOfJuly))
        {
            month += 2;
        }
        else
        {
            month++;
        }
    }

    /// <summary>
    /// Adds an instance to the list of Collections.
    /// </summary>
    /// <param name="collections">The list of collection days.</param>
    /// <param name="collectionTypes">The types of collection on a specific day.</param>
    /// <param name="year">Year.</param>
    /// <param name="month">Month.</param>
    /// <param name="dayIndex">Day.</param>
    /// <param name="property">The type of property.</param>
    private void Insert(List<Collection> collections, string[] collectionTypes, int year, int month, int dayIndex, int areaId)
    {
        for (int i = 1; i < collectionTypes.Length; i++)
        {
            collections.Add(Converter.ToCollection(
                year: year,
                month: month,
                day: dayIndex,
                code: collectionTypes[i],
                areaId: areaId,
                id: _id++
            ));
        }
    }

    /// <summary>
    /// Reads a line.
    /// </summary>
    /// <param name="collections">The list of collection days.</param>
    /// <param name="line">Current line.</param>
    /// <param name="day">Day.</param>
    /// <param name="year">Year.</param>
    /// <param name="property">The type of property.</param>
    private void ReadLine(List<Collection> collections, string line, int day, int year, int areaId)
    {
        int month = 1;
        string[] days = line.Split(day.ToString());
        for (int index = 1; index < days.Length; index++)
        {
            string[] collectionTypes = days[index].Trim().Split(' ');
            if (collectionTypes.Length > 1)
            {
                Insert(
                    collections: collections,
                    collectionTypes: collectionTypes,
                    year: year,
                    month: month,
                    dayIndex: day,
                    areaId: areaId
                );
            }

            Increase(month: ref month, day: day, index: index, year: year);
        }
    } 
}
