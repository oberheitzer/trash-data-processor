using System.Text.Json;
using TDP.Domain.Model;
using TDP.Http.Helpers;
using TDP.Http.Interfaces;

namespace TDP.Http.Services;

internal sealed class DatabaseService : IDatabaseService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options;

    public DatabaseService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };
    }

    public async Task<List<Calendar>> GetCalendarsAsync()
    {
        var response = await _httpClient.GetAsync(requestUri: Constant.CalendarUri);
        return JsonSerializer.Deserialize<List<Calendar>>(json: await response.Content.ReadAsStringAsync(), options: _options) ?? [];
    }
}
