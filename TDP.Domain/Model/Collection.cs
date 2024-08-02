using TDP.Domain.Enum;

namespace TDP.Domain.Model;

/// <summary>
/// Describes a waste collection.
/// </summary>
public class Collection
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The day of the collection.
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// The type of property or plot where the waste is collected from.
    /// </summary>
    public Property Property { get; set; }

    /// <summary>
    /// The type of the waste that is collected.
    /// </summary>
    public Waste Waste { get; set; }

    /// <summary>
    /// Foreign key.
    /// </summary>
    public int AreaId { get; set; }
    
    /// <summary>
    /// Navigation property.
    /// </summary>
    public Area Area { get; set; } = null!;
}
