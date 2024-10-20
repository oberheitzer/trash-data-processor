using TDP.Domain.Model;

namespace TDP.Http.Interfaces;

public interface IDatabaseService
{
    /// <summary>
    /// Gets all areas from database.
    /// </summary>
    /// <returns>List of areas.</returns>
    Task<List<Area>> GetAreasAsync();

    /// <summary>
    /// Gets the metadata of the calendars.
    /// </summary>
    /// <returns>List of calendars' metadata.</returns>
    Task<List<Calendar>> GetCalendarsAsync();
}
