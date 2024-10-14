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
    private readonly Mock<IFileSystem> _fileSystemMock = new();

    [TestMethod]
    public async Task DownloadAsync_Should_Copy_The_Content_Of_The_Downloaded_File()
    {
        // Arrange
        string content = "Test content";

        var fileSystem = new MockFileSystem();

        _ = _handlerMock
            .SetupSendAsync()
            .ReturnsAsync(Builder.BuildResponse(value: content));

        Builder.BuildFileMocks(fileSystem, _fileSystemMock);

        var service = new WasteService(
            httpClient: Builder.CreateClient(handler: _handlerMock.Object),
            fileSystem: _fileSystemMock.Object);

        // Act
        await service.DownloadAsync();

        // Assert
        MockFileData file = fileSystem.GetFile("test.pdf");
        file.TextContents.Should().Contain(content);
    }

    [TestMethod]
    public async Task DownloadAsync_Should_Not_Copy_Anything()
    {
        // Arrange
        var fileSystem = new MockFileSystem();

        _ = _handlerMock
            .SetupSendAsync()
            .ReturnsAsync(Builder.BuildResponse(value: null, code: HttpStatusCode.NotFound));

        Builder.BuildFileMocks(fileSystem, _fileSystemMock);

        var service = new WasteService(
            httpClient: Builder.CreateClient(handler: _handlerMock.Object),
            fileSystem: _fileSystemMock.Object);

        // Act
        await service.DownloadAsync();

        // Assert
        MockFileData file = fileSystem.GetFile("test.pdf");
        file.TextContents.Should().BeEmpty();
    }
}
