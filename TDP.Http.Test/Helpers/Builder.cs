using System.Net;
using System.Text.Json;
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

    internal static HttpResponseMessage BuildResponse(object? value, HttpStatusCode code = HttpStatusCode.OK)
    {
        return new HttpResponseMessage
        {
            Content = new StringContent(JsonSerializer.Serialize(value: value)),
            StatusCode = code
        };
    }

    internal static HttpClient CreateClient(HttpMessageHandler handler)
        => new(handler: handler) { BaseAddress = new Uri(uriString: "https://fake-waste-db.api") };
}
