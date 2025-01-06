using TDP.Extractor.Helpers;

namespace TDP.Extractor.Test;

[TestClass]
public class ConverterTests
{
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
