using TDP.Http.Helpers;
using TDP.Http.Interfaces;

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
        var response = await _httpClient.GetAsync(requestUri: $"{Constant.Folder}{Constant.GardonyArea16}");
        var directory = Directory.CreateDirectory(path: DirectoryExtension.GetDirectoryPath(folderName: Constant.Directory));
        using var file = File.Create(path: $@"{directory.FullName}/test.pdf");
        var content = await response.Content.ReadAsStreamAsync();
        await content.CopyToAsync(file);
    }
}
