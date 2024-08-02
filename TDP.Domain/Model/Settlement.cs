namespace TDP.Domain.Model;

/// <summary>
/// Describes a settlement.
/// </summary>
public class Settlement
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name of the settlement.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Foreign key.
    /// </summary>
    public int CompanyId { get; set; }
    
    /// <summary>
    /// Navigation property.
    /// </summary>
    public Company Company { get; set; } = null!;

    /// <summary>
    /// Areas.
    /// </summary>
    public List<Area> Areas { get; set; } = [];
}
