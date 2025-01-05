using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;
using TDP.Extractor.Services;

namespace TDP.Extractor.Test;

[TestClass]
public class CalendarServiceTests
{
    [TestMethod]
    public void Write_Should_Only_Contain_The_Header_Line_When_File_Does_Not_Exist_And_Collection_Is_Empty()
    {
        // Arrange
        var fileSystem = new MockFileSystem();
        fileSystem.AddFile(path: "Test.sln", mockFile: new MockFileData(textContents: "Test"));
        fileSystem.AddDirectory("Data");
        var service = new CalendarService(fileSystem: fileSystem);
        
        // Act
        service.Write(collections: []);

        // Assert
        string text = fileSystem.File.ReadAllText("Data/collections.csv");
        text.Should().NotBeEmpty();
        text.Should().Contain("id");
        text.Should().Contain("date");
        text.Should().Contain("waste");
        text.Should().Contain("area_id");
    }

    [TestMethod]
    public void Write_Should_Contain_Data_Lines_When_File_Does_Not_Exist()
    {
        // Arrange
        var fileSystem = new MockFileSystem();
        fileSystem.AddFile(path: "Test.sln", mockFile: new MockFileData(textContents: "Test"));
        fileSystem.AddDirectory("Data");
        var service = new CalendarService(fileSystem: fileSystem);

        // Act
        service.Write(collections: [
            new Domain.Model.Collection { Id = 1, CalendarId = 1, Date = new DateOnly(2024, 11, 5), Waste = Domain.Enum.Waste.Solid },
            new Domain.Model.Collection { Id = 2, CalendarId = 1, Date = new DateOnly(2024, 11, 4), Waste = Domain.Enum.Waste.Recyclable }
        ]);

        // Assert
        string[] lines = fileSystem.File.ReadAllLines("Data/collections.csv");
        lines.Should().NotBeEmpty();
        lines.Length.Should().Be(3);
        string[] properties = lines[1].Split(',');
        Assert.AreEqual(expected: properties[0], actual: "1");
        Assert.AreEqual(expected: properties[1], actual: "11/05/2024");
        Assert.AreEqual(expected: properties[2], actual: "0");
        Assert.AreEqual(expected: properties[3], actual: "1");
    }
}