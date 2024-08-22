using TDP.Domain.Enum;

namespace TDP.Extractor.Helpers;

public static class Converter
{
    public static Property ToProperty(string line) => line.ToLower() switch
    {
        string text when text.Contains(Constant.HolidayHouse) => Property.HolidayHouse,
        string text when text.Contains(Constant.EmptyPlot) => Property.EmptyPlot,
        string text when text.Contains(Constant.GardenPlot) => Property.GardenPlot,
        // "üres" => Property.EmptyPlot,
        // "zártkert" => Property.GardenPlot,
        _ => Property.PermanentAddress,
        // "" => Property.PermanentAddress,
        // _ => throw new ArgumentOutOfRangeException(nameof(property), $"Not expected property value: {property}"),
    };

    public static Waste ToWaste(string code) => code switch
    {
        Constant.SolidWaste => Waste.Solid,
        Constant.OrganicWaste => Waste.Organic,
        Constant.RecyclableWaste => Waste.Recyclable,
        _ => throw new ArgumentOutOfRangeException(nameof(code), $"Not expected code value: {code}"),
    };
}
