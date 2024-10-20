﻿using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Net;
using System.Text.Json;
using Moq;
using TDP.Domain.Model;

namespace TDP.Http.Test;

internal static class Builder
{
    internal static List<Area> BuildAreas()
    {
        return
        [
            new Area { Id = 1, Name = "I", SettlementId = 1 },
            new Area { Id = 4, Name = "IV", SettlementId = 1 },
            new Area { Id = 13, Name = "XIII", SettlementId = 1 },
        ];
    }

    internal static List<Calendar> BuildCalendars()
    {
        return
        [
            new Calendar { Id = 1, Name = "Test 1", SettlementId = 1, Uri = "/test-path-one" },
            new Calendar { Id = 1, Name = "Test 2", SettlementId = 1, Uri = "/test-path-two" }
        ];
    }

    internal static void BuildFileMocks(MockFileSystem fileSystem, Mock<IFileSystem> fileSystemMock)
    {
         _ = fileSystemMock
            .Setup(fileSystem => fileSystem.Directory.CreateDirectory(It.IsAny<string>()))
            .Returns(fileSystem.Directory.CreateDirectory(path: "Test"));

        _ = fileSystemMock
            .Setup(fileSystem => fileSystem.File.Create(It.IsAny<string>()))
            .Returns(fileSystem.File.Create(path: "test.pdf"));
    }

    internal static HttpResponseMessage BuildResponse(object? value, HttpStatusCode code = HttpStatusCode.OK)
    {
        return new HttpResponseMessage(statusCode: code)
        {
           Content = new StringContent(JsonSerializer.Serialize(value: value))
        };
    }

    internal static HttpClient CreateClient(HttpMessageHandler handler)
        => new(handler: handler) { BaseAddress = new Uri(uriString: "https://fake-waste-db.api") };
}
