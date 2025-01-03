using TDP.Domain.Enum;
using TDP.Domain.Model;

namespace TDP.Extractor.Helpers;

public static class Converter
{
    /// <summary>
    /// Finds and returns the name of the area.
    /// </summary>
    /// <param name="line">Title line of the calendar.</param>
    /// <returns>Name of area.</returns>
    public static string ToArea(string line)
    {
        // TODO more settlement
        string area = line.Split("Gárdony")[1].Trim();
        int dotIndex = area.IndexOf('.');
        if (dotIndex != -1)
        {
            return area.Remove(dotIndex);
        }
        return area;
    }

    /// <summary>
    /// Finds and returns the identifier of the area based of its given name.
    /// </summary>
    /// <param name="area">Name of the area.</param>
    /// <param name="areas">List of areas.</param>
    /// <returns>The identifier of the area.</returns>
    public static int ToAreaId(string area, List<Area> areas)
        => areas.Single(a => area.ToLower() == a.Name.ToLower()).Id;

    /// <summary>
    /// Creates and instance of the Collection class.
    /// </summary>
    /// <param name="year">Year of collection.</param>
    /// <param name="month">Month of collection.</param>
    /// <param name="day">Day of collection.</param>
    /// <param name="code">The code of the type of waste.</param>
    /// <param name="property">The type of the property.</param>
    /// <returns>Created instance of Collection.</returns>
    public static Collection ToCollection(int year, int month, int day, string code, int areaId, int id)
    {
        return new Collection
        {
            AreaId = areaId,
            Date = new DateOnly(year: year, month: month, day: day),
            Id = id,
            Waste = ToWaste(code: code)
        };
    }

    public static Waste ToWaste(string code) => code switch
    {
        Constant.SolidWaste => Waste.Solid,
        Constant.OrganicWaste => Waste.Organic,
        Constant.RecyclableWaste => Waste.Recyclable,
        _ => throw new ArgumentOutOfRangeException(nameof(code), $"Not expected code value: {code}"),
    };
}
