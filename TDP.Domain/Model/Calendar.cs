namespace TDP.Domain.Model;

/// <summary>
/// Metadata of a calendar.
/// </summary>
public class Calendar
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The specific part of the Uri where the calendar is located.
    /// </summary>
    public string Uri { get; set; } = string.Empty;

    /// <summary>
    /// The given name of the downloaded file.
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
}
