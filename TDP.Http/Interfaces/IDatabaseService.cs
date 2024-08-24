using TDP.Domain.Model;

namespace TDP.Http.Interfaces;

public interface IDatabaseService
{
    /// <summary>
    /// Gets all areas from database.
    /// </summary>
    /// <returns>List of areas.</returns>
    Task<List<Area>> GetAreasAsync();
}
