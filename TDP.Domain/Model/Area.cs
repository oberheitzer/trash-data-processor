namespace TDP.Domain.Model;

/// <summary>
/// Describes an area (group of public places) in a settlement.
/// </summary>
public class Area
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name of the area.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Foreign key.
    /// </summary>
    public int SettlementId { get; set; }
    
    /// <summary>
    /// Navigation property.
    /// </summary>
    public Settlement Settlement { get; set; } = null!;

    /// <summary>
    /// Streets.
    /// </summary>
    public List<Street> Streets { get; set; } = [];

    /// <summary>
    /// Collections.
    /// </summary>
    public List<Collection> Collections { get; set; } = [];
}
