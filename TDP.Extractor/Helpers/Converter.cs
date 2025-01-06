namespace TDP.Extractor.Helpers;

public static class Converter
{
    public static DayOfWeek ToDayOfWeek(string day) => day.ToLower() switch
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

    public static int ToMonthIndex(string month) => month.ToLower() switch
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
