namespace TDP.Http.Interfaces;

public interface IWasteService
{
    /// <summary>
    /// Downloads the calendar files and saves them in a temporary folder.
    /// </summary>
    Task DownloadAsync();
}
