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
}
