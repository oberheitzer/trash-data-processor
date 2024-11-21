using System.IO.Abstractions;
using System.Net;
using TDP.Domain.Model;
using TDP.Http.Helpers;
using TDP.Http.Interfaces;

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

    public async Task DownloadAsync(List<Calendar> calendars)
    {
        var directory = _fileSystem.Directory.CreateDirectory(path: _fileSystem.GetDirectoryPath(folderName: Shared.Constants.File.Calendars));

        foreach (Calendar calendar in calendars)
        {
            var response = await _httpClient.GetAsync(requestUri: $"{Constant.Folder}{calendar.Uri}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                using var file = _fileSystem.File.Create(path: $@"{directory.FullName}/{calendar.Name}.pdf");
                var content = await response.Content.ReadAsStreamAsync();
                await content.CopyToAsync(file);
                content.Position = 0;
            }
        }
    }
}
