using TDP.Domain.Enum;
using TDP.Domain.Model;
using TDP.Extractor.Helpers;

namespace TDP.Extractor.Test;

[TestClass]
public class ConverterTests
{
    [DataTestMethod]
    [DataRow("2024. évi hulladéknaptár - Gárdony I.", "I")]
    [DataRow("2024. évi hulladéknaptár - Gárdony Zártkert", "Zártkert")]
    public void ToArea_Should_Work(string line, string expected)
    {
        // Arrange & Act
        string area = Converter.ToArea(line: line);

        // Assert
        Assert.AreEqual(expected: expected, actual: area);
    }

    [DataTestMethod]
    [DataRow("I", 1)]
    [DataRow("IV", 4)]
    [DataRow("XIII", 13)]
    [DataRow("xiIi", 13)]
    [DataRow("Zártkert", 17)]
    [DataRow("zártkert", 17)]
    public void ToAreaId_Should_Work(string area, int expected)
    {
        // Arrange
        List<Area> areas = Builder.BuildAreas();

        // Act
        int areaId = Converter.ToAreaId(area: area, areas: areas);

        // Assert
        Assert.AreEqual(expected: expected, actual: areaId);
    }

    [DataTestMethod]
    [DataRow("üdülő", Property.HolidayHouse)]
    [DataRow("Üdülő", Property.HolidayHouse)]
    [DataRow("ÜDÜLŐ", Property.HolidayHouse)]
    [DataRow("üres", Property.EmptyPlot)]
    [DataRow("Üres", Property.EmptyPlot)]
    [DataRow("ÜRES", Property.EmptyPlot)]
    [DataRow("zártkert", Property.GardenPlot)]
    [DataRow("Zártkert", Property.GardenPlot)]
    [DataRow("ZÁRTKERT", Property.GardenPlot)]
    [DataRow("", Property.PermanentAddress)]
    public void ToProperty_Should_Work(string property, Property expected)
    {
        // Arrange
        string line = $"2024. évi hulladéknaptár - Teszt {property}";

        // Act
        Property convertedValue = Converter.ToProperty(line: line);

        // Assert
        Assert.AreEqual(expected: expected, actual: convertedValue);
    }

    [DataTestMethod]
    [DataRow(Constant.SolidWaste, Waste.Solid)]
    [DataRow(Constant.OrganicWaste, Waste.Organic)]
    [DataRow(Constant.RecyclableWaste, Waste.Recyclable)]
    public void ToWaste_Should_Work(string code, Waste expected)
    {
        // Arrange & Act
        Waste convertedValue = Converter.ToWaste(code: code);

        // Assert
        Assert.AreEqual(expected: expected, actual: convertedValue);
    }

    [TestMethod]
    public void ToWaste_Should_Throw_Exception()
    {
        // Arrange
        string code = "Not a valid code";
        string expectedMessage = $"Not expected code value: {code}";

        // Act & Assert
        var exception = Assert.ThrowsException<ArgumentOutOfRangeException>(() => Converter.ToWaste(code: code));
        Assert.IsTrue(condition: exception.Message.Contains(expectedMessage));
    }
}
