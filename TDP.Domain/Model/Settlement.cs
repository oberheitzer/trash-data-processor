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
    /// Foreign key.
    /// If the settlement is part of an other settlement, this value is not null.
    /// </summary>
    public int? SettlementId { get; set; }

    /// <summary>
    /// Calendars.
    /// </summary>
    public List<Calendar> Calendars { get; set; } = [];

    /// <summary>
    /// Streets.
    /// The streets that belong to the settlement.
    /// </summary>
    public List<Street> Streets { get; set; } = [];
}
