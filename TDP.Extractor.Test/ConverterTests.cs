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

    [TestMethod]
    public void ToCollection_Should_Work()
    {
        // Arrange & Act
        Collection collection = Converter.ToCollection(
            year: 2024,
            month: 6,
            day: 15,
            code: "V",
            areaId: 1,
            id: 1
        );

        // Assert
        Assert.AreEqual(expected: new DateOnly(2024, 6, 15), actual: collection.Date);
        Assert.AreEqual(expected: 1, actual: collection.Id);
        Assert.AreEqual(expected: 1, actual: collection.AreaId);
        Assert.AreEqual(expected: Waste.Solid, actual: collection.Waste);
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
