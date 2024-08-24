using System.Text.Json;
using TDP.Domain.Model;
using TDP.Http.Helpers;
using TDP.Http.Interfaces;

namespace TDP.Http.Services;

internal sealed class DatabaseService : IDatabaseService
{
    private readonly HttpClient _httpClient;

    public DatabaseService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Area>> GetAreasAsync()
    {
        var response = await _httpClient.GetAsync(requestUri: Constant.AreaUri);
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };
        return JsonSerializer.Deserialize<List<Area>>(json: await response.Content.ReadAsStringAsync(), options: options) ?? [];
    }
}
