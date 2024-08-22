using System.Collections.ObjectModel;
using System.Text;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using TDP.Domain.Enum;
using TDP.Domain.Model;
using TDP.Extractor.Helpers;
using TDP.Extractor.Interfaces;

namespace TDP.Extractor.Services;

internal sealed class CalendarService : ICalendarService
{
    private int _id = 1;
    private const int FIRST_PAGE = 1;
    private const int FIRST_DAY = 1;
    private const int LAST_DAY = 31;

    public void Read(string file)
    {
        using PdfReader reader = new(filename: file);
        using PdfDocument document = new(reader: reader);
        // StringWriter stringWriter = new();
        var strategy = new SimpleTextExtractionStrategy();
        string text = PdfTextExtractor.GetTextFromPage(page: document.GetPage(pageNum: FIRST_PAGE), strategy: strategy);
        // stringWriter.Write(value: text);
        // var r = stringWriter.ToString();
        string[] lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        StringBuilder sb = new();
        int year = DateTime.Now.Year;
        Property property = Property.EmptyPlot;
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
        string calendar = sb.ToString();

        string[] dayLines = calendar.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        List<Collection> collections = [];
        int dayIndex = 1;
        foreach (string dayLine in dayLines)
        {
            int month = 1;
            string[] days = dayLine.Split(dayIndex.ToString());
            for (int index = 1; index < days.Length; index++)
            {
                string[] _collections = days[index].Trim().Split(' '); // TODO naming
                if (_collections.Length > 1)
                {
                    for (int i = 1; i < _collections.Length; i++)
                    {
                        Waste waste = Converter.ToWaste(code: _collections[i]);
                        collections.Add(new Collection
                        {
                            AreaId = 16, // TODO
                            Date = new DateOnly(year: year, month: month, day: dayIndex), // TODO year?
                            Id = _id++, // TODO
                            Property = property,// Property.PermanentAddress, // TODO,
                            Waste = waste
                        });
                    }
                }
                month++;
            }
            dayIndex++;
        }
    }
}
