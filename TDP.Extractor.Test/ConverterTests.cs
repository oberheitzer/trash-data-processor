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
        // TODO Assert.AreEqual(expected: 1, actual: collection.AreaId);
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

    [DataTestMethod]
    [DataRow("hétfő", DayOfWeek.Monday)]
    [DataRow("szerda", DayOfWeek.Wednesday)]
    [DataRow("vasárnap", DayOfWeek.Sunday)]
    public void ToDayOfWeek_Should_Work(string day, DayOfWeek expected)
    {
        // Arrange & Act
        DayOfWeek convertedValue = Converter.ToDayOfWeek(day: day);

        // Assert
        Assert.AreEqual(expected: expected, actual: convertedValue);
    }

    [DataTestMethod]
    [DataRow("Hétfő")]
    [DataRow("test")]
    public void ToDayOfWeek_Should_Throw_Exception(string day)
    {
        // Arrange
        string expectedMessage = $"Unexpected name of a day: {day}";

        // Act & Assert
        var exception = Assert.ThrowsException<ArgumentOutOfRangeException>(() => Converter.ToDayOfWeek(day: day));
        Assert.IsTrue(condition: exception.Message.Contains(expectedMessage));
    }

    [DataTestMethod]
    [DataRow("január", 1)]
    [DataRow("május", 5)]
    [DataRow("december", 12)]
    public void ToMonthIndex_Should_Work(string month, int expected)
    {
        // Arrange & Act
        int convertedValue = Converter.ToMonthIndex(month: month);

        // Assert
        Assert.AreEqual(expected: expected, actual: convertedValue);
    }

    [DataTestMethod]
    [DataRow("Április")]
    [DataRow("test")]
    public void ToMonthIndex_Should_Throw_Exception(string month)
    {
        // Arrange
        string expectedMessage = $"Unexpected name of a month: {month}";

        // Act & Assert
        var exception = Assert.ThrowsException<ArgumentOutOfRangeException>(() => Converter.ToMonthIndex(month: month));
        Assert.IsTrue(condition: exception.Message.Contains(expectedMessage));
    }
}
