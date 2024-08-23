using TDP.Domain.Model;

namespace TDP.Extractor.Interfaces;

public interface ICalendarService
{
    /// <summary>
    /// Reads the downloaded files.
    /// </summary>
    List<Collection> Read(string file);

    /// <summary>
    /// Writes the waste collection days into the specific csv file.
    /// </summary>
    /// <param name="collections"></param>
    void Write(List<Collection> collections);
}
