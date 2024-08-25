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
    // TODO check zártkert
        => areas.Single(a => area == a.Name).Id;

    public static Property ToProperty(string line) => line.ToLower() switch
    {
        string text when text.Contains(Constant.HolidayHouse) => Property.HolidayHouse,
        string text when text.Contains(Constant.EmptyPlot) => Property.EmptyPlot,
        string text when text.Contains(Constant.GardenPlot) => Property.GardenPlot,
        _ => Property.PermanentAddress,
    };

    public static Waste ToWaste(string code) => code switch
    {
        Constant.SolidWaste => Waste.Solid,
        Constant.OrganicWaste => Waste.Organic,
        Constant.RecyclableWaste => Waste.Recyclable,
        _ => throw new ArgumentOutOfRangeException(nameof(code), $"Not expected code value: {code}"),
    };
}
