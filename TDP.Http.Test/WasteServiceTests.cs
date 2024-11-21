using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Net;
using FluentAssertions;
using Moq;
using TDP.Domain.Model;
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

        List<Calendar> calendars = [
            new Calendar { Id = 1, Name = "test_one", SettlementId = 1, Uri = "/test-one.pdf" },
            new Calendar { Id = 2, Name = "test_two", SettlementId = 1, Uri = "/test-two.pdf" }
        ];

        _ = _handlerMock
            .SetupSendAsync()
            .ReturnsAsync(Builder.BuildResponse(value: content));

        var service = new WasteService(
            httpClient: Builder.CreateClient(handler: _handlerMock.Object),
            fileSystem: fileSystem);

        // Act
        await service.DownloadAsync(calendars: calendars);

        // Assert
        fileSystem.Directory.GetFiles("/Calendars").Length.Should().Be(2);
        string firstPdf = fileSystem.File.ReadAllText("/Calendars/test_one.pdf");
        string secondPdf = fileSystem.File.ReadAllText("/Calendars/test_two.pdf");
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
        await service.DownloadAsync([]);

        // Assert
        fileSystem.Directory.GetFiles("/Calendars").Length.Should().Be(0);
    }
}
