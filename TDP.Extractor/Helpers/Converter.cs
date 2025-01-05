using TDP.Domain.Enum;
using TDP.Domain.Model;

namespace TDP.Extractor.Helpers;

public static class Converter
{
    /// <summary>
    /// Finds and returns the name of the area.
    /// </summary>
    /// <param name="line">Title line of the calendar.</param>
    /// <returns>Name of area.</returns>
    public static string ToArea(string line)
    {
        // TODO more settlement
        string area = line.Split("Gárdony")[1].Trim();
        int dotIndex = area.IndexOf('.');
        if (dotIndex != -1)
        {
            return area.Remove(dotIndex);
        }
        return area;
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
    public static Collection ToCollection(int year, int month, int day, string code, int areaId, int id)
    {
        return new Collection
        {
            Date = new DateOnly(year: year, month: month, day: day),
            Id = id,
            Waste = ToWaste(code: code)
        };
    }

    public static Waste ToWaste(string code) => code switch
    {
        Constant.SolidWaste => Waste.Solid,
        Constant.OrganicWaste => Waste.Organic,
        Constant.RecyclableWaste => Waste.Recyclable,
        _ => throw new ArgumentOutOfRangeException(nameof(code), $"Not expected code value: {code}"),
    };

    public static DayOfWeek ToDayOfWeek(string day) => day switch
    {
        Week.Monday => DayOfWeek.Monday,
        Week.Tuesday => DayOfWeek.Tuesday,
        Week.Wednesday => DayOfWeek.Wednesday,
        Week.Thursday => DayOfWeek.Thursday,
        Week.Friday => DayOfWeek.Friday,
        Week.Saturday => DayOfWeek.Saturday,
        Week.Sunday => DayOfWeek.Sunday,
        _ => throw new ArgumentOutOfRangeException($"Unexpected name of a day: {day}")
    };

    public static int ToMonthIndex(string month) => month switch
    {
        Month.January => 1,
        Month.February => 2,
        Month.March => 3,
        Month.April => 4,
        Month.May => 5,
        Month.June => 6,
        Month.July => 7,
        Month.August => 8,
        Month.September => 9,
        Month.October => 10,
        Month.November => 11,
        Month.December => 12,
        _ => throw new ArgumentOutOfRangeException($"Unexpected name of a month: {month}")
    };
}
