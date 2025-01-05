using TDP.Domain.Model;

namespace TDP.Extractor.Interfaces;

public interface ICalendarService
{
    /// <summary>
    /// Reads the downloaded files.
    /// </summary>
    /// <param name="calendars">Calendars.</param>
    /// <returns>The collection days.</returns>
    IEnumerable<List<Collection>> Read(List<Calendar> calendars);

    /// <summary>
    /// Writes the waste collection days into the specific csv file.
    /// </summary>
    /// <param name="collections">The list of days when collection happens.</param>
    void Write(List<Collection> collections);
}
