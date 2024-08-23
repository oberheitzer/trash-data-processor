using System.Globalization;
using System.Text;
using CsvHelper;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using TDP.Domain.Enum;
using TDP.Domain.Model;
using TDP.Extractor.Helpers;
using TDP.Extractor.Interfaces;
using TDP.Extractor.Mappers;
using TDP.Shared.Extensions;

namespace TDP.Extractor.Services;

internal sealed class CalendarService : ICalendarService
{
    private int _id = 1;
    private const int FIRST_PAGE = 1;
    private const int FIRST_DAY = 1;
    private const int LAST_DAY = 31;

    public List<Collection> Read(string file)
    {
        using PdfReader reader = new(filename: file);
        using PdfDocument document = new(reader: reader);
        var strategy = new SimpleTextExtractionStrategy();
        string text = PdfTextExtractor.GetTextFromPage(page: document.GetPage(pageNum: FIRST_PAGE), strategy: strategy);
        string[] lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

        (string calendar, int year, Property property) = Extract(lines: lines);

        string[] dayLines = calendar.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        List<Collection> collections = [];
        int dayIndex = 1;

        foreach (string dayLine in dayLines)
        {
            ReadLine(
                collections: collections,
                line: dayLine,
                day: dayIndex,
                year: year,
                property: property
            );

            dayIndex++;
        }

        return collections;
    }

    public void Write(List<Collection> collections)
    {
        using StreamWriter writer = new(path: $"{DirectoryExtension.GetDirectoryPath(folderName: Shared.Constants.File.Data)}/{Shared.Constants.File.Collections}");
        using CsvWriter csv = new(writer: writer, culture: CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<CollectionMapper>();
        csv.WriteRecords(records: collections);
    }

    /// <summary>
    /// Extract the necessary data from the downloaded PDF file.
    /// </summary>
    /// <param name="lines">All lines from the file.</param>
    /// <returns>The calendar lines, the year and the type of the property of waste collection.</returns>
    private (string calendar, int year, Property property) Extract(string[] lines)
    {
        StringBuilder sb = new();
        int year = DateTime.Now.Year;
        Property property = Property.EmptyPlot; // TODO default value?
        foreach (string line in lines)
        {
            string day = line.Split(' ')[0];
            bool isNumber = int.TryParse(day, out int _day);
            if (isNumber && _day >= FIRST_DAY && _day <= LAST_DAY)
            {
                sb.AppendLine(line);
            }

            if (line.Contains(Constant.WasteCalendar))
            {
                Console.WriteLine(line);
                property = Converter.ToProperty(line: line);
                bool isYear = int.TryParse(line.Substring(startIndex: 0, length: line.IndexOf('.')), out int _year);
                if (isYear && _year / 1000 >= 1) // TODO second condition
                {
                    year = _year;
                }
            }
        }
        return (calendar: sb.ToString(), year, property);
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
    private void Insert(List<Collection> collections, string[] collectionTypes, int year, int month, int dayIndex, Property property)
    {
        for (int i = 1; i < collectionTypes.Length; i++)
        {
            collections.Add(ToCollection(
                year: year,
                month: month,
                day: dayIndex,
                code: collectionTypes[i],
                property: property
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
    private void ReadLine(List<Collection> collections, string line, int day, int year, Property property)
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
                    property: property
                );
            }
            month++;
        }
    }

    /// <summary>
    /// Creates and instance of the Collection class.
    /// </summary>
    /// <param name="year">Year of collection.</param>
    /// <param name="month">Month of collection.</param>
    /// <param name="day">Day of collection.</param>
    /// <param name="code">The code of the type of waste.</param>
    /// <param name="property">The type of the property.</param>
    /// <returns>Created instance of Collection.</returns>
    private Collection ToCollection(int year, int month, int day, string code, Property property)
    {
        return new Collection
        {
            AreaId = 16, // TODO
            Date = new DateOnly(year: year, month: month, day: day),
            Id = _id++, // TODO
            Property = property,
            Waste = Converter.ToWaste(code: code)
        };
    }
}
