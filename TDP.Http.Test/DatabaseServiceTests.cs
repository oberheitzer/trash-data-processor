using System.Net;
using FluentAssertions;
using Moq;
using TDP.Http.Services;

namespace TDP.Http.Test;

[TestClass]
public class DatabaseServiceTests
{
    private readonly Mock<HttpMessageHandler> _handlerMock = new();

    [TestMethod]
    public async Task GetAreasAsync_Should_Work()
    {
        // Arrange
        _ = _handlerMock
            .SetupSendAsync()
            .ReturnsAsync(Builder.BuildResponse(value: Builder.BuildAreas())); 

        var service = new DatabaseService(Builder.CreateClient(handler: _handlerMock.Object));    

        // Act
        var response = await service.GetAreasAsync();

        // Assert
        response.Should().HaveCount(3);
    }

    [TestMethod]
    public async Task GetAreasAsync_Should_Return_Empty_List()
    {
        // Arrange
        _ = _handlerMock
            .SetupSendAsync()
            .ReturnsAsync(Builder.BuildResponse(value: null, code: HttpStatusCode.BadRequest)); 

        var service = new DatabaseService(Builder.CreateClient(handler: _handlerMock.Object));    

        // Act
        var response = await service.GetAreasAsync();

        // Assert
        response.Should().BeEmpty();
    }
}