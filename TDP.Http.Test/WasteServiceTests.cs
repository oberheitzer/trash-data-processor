using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Net;
using FluentAssertions;
using Moq;
using TDP.Http.Services;

namespace TDP.Http.Test;

[TestClass]
public class WasteServiceTests
{
    private readonly Mock<HttpMessageHandler> _handlerMock = new();

    [TestMethod]
    public async Task DownloadAsync_Should_Copy_The_Content_Of_The_Downloaded_File()
    {
        // Arrange
        string content = "Test content";

        var fileSystem = new MockFileSystem();
        fileSystem.AddFile(path: "Test.sln", mockFile: new MockFileData(textContents: "Test"));

        _ = _handlerMock
            .SetupSendAsync()
            .ReturnsAsync(Builder.BuildResponse(value: content));

        var service = new WasteService(
            httpClient: Builder.CreateClient(handler: _handlerMock.Object),
            fileSystem: fileSystem);

        // Act
        await service.DownloadAsync();

        // Assert
        string firstPdf = fileSystem.File.ReadAllText("/Calendars/Gardony_IV.pdf");
        string secondPdf = fileSystem.File.ReadAllText("/Calendars/Gardony_XVI.pdf");
        firstPdf.Should().Contain(content);
        secondPdf.Should().Contain(content);
    }

    [TestMethod]
    public async Task DownloadAsync_Should_Not_Copy_Anything()
    {
        // Arrange
        var fileSystem = new MockFileSystem();
        fileSystem.AddFile(path: "Test.sln", mockFile: new MockFileData(textContents: "Test"));

        _ = _handlerMock
            .SetupSendAsync()
            .ReturnsAsync(Builder.BuildResponse(value: null, code: HttpStatusCode.NotFound));

        var service = new WasteService(
            httpClient: Builder.CreateClient(handler: _handlerMock.Object),
            fileSystem: fileSystem);

        // Act
        await service.DownloadAsync();

        // Assert
        fileSystem.Directory.GetFiles("/Calendars").Length.Should().Be(0);
    }
}
