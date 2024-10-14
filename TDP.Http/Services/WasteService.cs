using System.IO.Abstractions;
using System.Net;
using TDP.Http.Helpers;
using TDP.Http.Interfaces;
using TDP.Shared.Extensions;

namespace TDP.Http.Services;

internal sealed class WasteService : IWasteService
{
    private readonly HttpClient _httpClient;
    private readonly IFileSystem _fileSystem;

    public WasteService(HttpClient httpClient, IFileSystem fileSystem)
    {
        _httpClient = httpClient;
        _fileSystem = fileSystem;
    }

    public async Task DownloadAsync()
    {
        var directory = _fileSystem.Directory.CreateDirectory(path: DirectoryExtension.GetDirectoryPath(folderName: Shared.Constants.File.Calendars));
        
        foreach ((string fileName, string requestUri) in Shared.Constants.Uri.Areas)
        {
            var response = await _httpClient.GetAsync(requestUri: $"{Constant.Folder}{requestUri}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                using var file = _fileSystem.File.Create(path: $@"{directory.FullName}/{fileName}.pdf");
                var content = await response.Content.ReadAsStreamAsync();
                await content.CopyToAsync(file);
            }
        }
    }
}
