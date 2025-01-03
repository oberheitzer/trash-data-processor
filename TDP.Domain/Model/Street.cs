namespace TDP.Domain.Model;

/// <summary>
/// Describes a public place.
/// </summary>
public class Street
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name of the public place.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Foreign key.
    /// </summary>
    public int AreaId { get; set; }

    /// <summary>
    /// Navigation property.
    /// </summary>
    public Area Area { get; set; } = null!;

    /// <summary>
    /// Foreign key.
    /// The settlement where the street belongs to.
    /// </summary>
    public int SettlementId { get; set; }

    /// <summary>
    /// Navigation property.
    /// </summary>
    public Settlement Settlement { get; set; } = null!;
}
