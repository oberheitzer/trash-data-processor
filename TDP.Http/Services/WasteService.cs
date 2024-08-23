using TDP.Http.Helpers;
using TDP.Http.Interfaces;
using TDP.Shared.Extensions;

namespace TDP.Http.Services;

internal sealed class WasteService : IWasteService
{
    private readonly HttpClient _httpClient;

    public WasteService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task DownloadAsync()
    {
        var directory = Directory.CreateDirectory(path: DirectoryExtension.GetDirectoryPath(folderName: Shared.Constants.File.Calendars));
        
        foreach ((string fileName, string requestUri) in Shared.Constants.Uri.Areas)
        {
            var response = await _httpClient.GetAsync(requestUri: $"{Constant.Folder}{requestUri}");
            using var file = File.Create(path: $@"{directory.FullName}/{fileName}.pdf");
            var content = await response.Content.ReadAsStreamAsync();
            await content.CopyToAsync(file);
        }
    }
}
