namespace TDP.Domain.Model;

/// <summary>
/// Describes the waste collection company.
/// </summary>
public class Company
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The short version (abbreviation) of the company's name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The full name of the company.
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// The settlements where the company collects the waste.
    /// </summary>
    public List<Settlement> Settlements { get; set; } = [];
}
