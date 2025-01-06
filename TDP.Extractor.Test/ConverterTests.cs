using TDP.Domain.Model;
using TDP.Extractor.Helpers;

namespace TDP.Extractor.Test;

[TestClass]
public class ConverterTests
{
    [TestMethod]
    public void ToCollection_Should_Work()
    {
        // Arrange & Act
        Collection collection = Converter.ToCollection(
            id: 1,
            calendarId: 1,
            monthIndex: 12,
            day: 31,
            waste: Domain.Enum.Waste.Solid
        );

        // Assert
        Assert.AreEqual(expected: 1, actual: collection.Id);
        Assert.AreEqual(expected: 1, actual: collection.CalendarId);
        Assert.AreEqual(expected: Domain.Enum.Waste.Solid, actual: collection.Waste);
        Assert.AreEqual(expected: new DateOnly(2025, 12, 31), actual: collection.Date);
    }

    [TestMethod]
    public void ToCollection_Without_Date_Should_Work()
    {
        // Arrange & Act
        Collection collection = Converter.ToCollection(
            id: 1,
            calendarId: 1,
            waste: Domain.Enum.Waste.Solid
        );

        // Assert
        Assert.AreEqual(expected: 1, actual: collection.Id);
        Assert.AreEqual(expected: 1, actual: collection.CalendarId);
        Assert.AreEqual(expected: Domain.Enum.Waste.Solid, actual: collection.Waste);
        Assert.AreEqual(expected: DateOnly.MinValue , actual: collection.Date);
    }

    [TestMethod]
    public void ToDate_Should_Work()
    {
        // Arrange & Act
        DateOnly date = Converter.ToDate(
            month: "December",
            day: "31"
        );

        // Assert
        Assert.AreEqual(expected: new DateOnly(2025,12,31), actual: date);
    }

    [DataTestMethod]
    [DataRow("hétfő", DayOfWeek.Monday)]
    [DataRow("Kedd", DayOfWeek.Tuesday)]
    [DataRow("szerda", DayOfWeek.Wednesday)]
    [DataRow("CSÜTÖRTÖK", DayOfWeek.Thursday)]
    [DataRow("PéNtEk", DayOfWeek.Friday)]
    [DataRow("vasárnap", DayOfWeek.Sunday)]
    public void ToDayOfWeek_Should_Work(string day, DayOfWeek expected)
    {
        // Arrange & Act
        DayOfWeek convertedValue = Converter.ToDayOfWeek(day: day);

        // Assert
        Assert.AreEqual(expected: expected, actual: convertedValue);
    }

    [DataTestMethod]
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
    [DataRow("Április", 4)]
    [DataRow("május", 5)]
    [DataRow("aUgUszTuS", 8)]
    [DataRow("OKTÓBER", 10)]
    [DataRow("december", 12)]
    public void ToMonthIndex_Should_Work(string month, int expected)
    {
        // Arrange & Act
        int convertedValue = Converter.ToMonthIndex(month: month);

        // Assert
        Assert.AreEqual(expected: expected, actual: convertedValue);
    }

    [DataTestMethod]
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
