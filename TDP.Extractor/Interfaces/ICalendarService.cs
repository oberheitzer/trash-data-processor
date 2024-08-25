using TDP.Domain.Model;

namespace TDP.Extractor.Interfaces;

public interface ICalendarService
{
    /// <summary>
    /// Reads the downloaded files.
    /// </summary>
    /// <param name="file">The downloaded file.</param>
    IEnumerable<List<Collection>> Read(List<Area> areas);

    /// <summary>
    /// Writes the waste collection days into the specific csv file.
    /// </summary>
    /// <param name="collections">The list of days when collection happens.</param>
    void Write(List<Collection> collections);
}
