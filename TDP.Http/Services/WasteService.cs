using TDP.Http.Interfaces;

namespace TDP.Http.Services;

internal sealed class WasteService : IWasteService
{
    private readonly HttpClient _httpClient;

    public WasteService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
}
