using TDP.Domain.Enum;
using TDP.Domain.Model;

namespace TDP.Extractor.Helpers;

public static class Converter
{
    /// <summary>
    /// Returns with a Collection instance.
    /// </summary>
    /// <param name="id">Unique identifier.</param>
    /// <param name="calendarId">Unique identifier of Calendar.</param>
    /// <param name="monthIndex">Index of month.</param>
    /// <param name="day">Index of day.</param>
    /// <param name="waste">Type of waste.</param>
    /// <returns>Collection.</returns>
    public static Collection ToCollection(int id, int calendarId, int monthIndex, int day, Waste waste)
        => new()
        {
            Id = id,
            CalendarId = calendarId,
            Date = new DateOnly(year: 2025, month: monthIndex, day: day),
            Waste = waste
        };

    /// <summary>
    /// Returns with a Collection instance.
    /// </summary>
    /// <param name="id">Unique identifier.</param>
    /// <param name="calendarId">Unique identifier of Calendar.</param>
    /// <param name="waste">Type of waste.</param>
    /// <returns>Collection.</returns>
    public static Collection ToCollection(int id, int calendarId, Waste waste)
        => new()
        {
            Id = id,
            CalendarId = calendarId,
            Waste = waste
        };

    /// <summary>
    /// Returns a DateOnly instance.
    /// </summary>
    /// <param name="month">Name of the month.</param>
    /// <param name="day">The number of the day.</param>
    /// <returns>The date.</returns>
    public static DateOnly ToDate(string month, string day)
        => new(
            year: 2025,
            month: ToMonthIndex(month: month),
            day: int.Parse(day));

    /// <summary>
    /// Returns the day of week based on the Hungarian name of the day.
    /// </summary>
    /// <param name="day">Name of the day.</param>
    /// <returns>Day of week.</returns>
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

    /// <summary>
    /// Returns the index of the month based on the Hungarian name of the month.
    /// </summary>
    /// <param name="month">Name of the month.</param>
    /// <returns>Index of the month.</returns>
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
