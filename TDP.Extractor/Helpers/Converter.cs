using TDP.Domain.Enum;

namespace TDP.Extractor.Helpers;

public static class Converter
{
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
