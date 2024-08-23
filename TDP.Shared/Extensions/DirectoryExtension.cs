namespace TDP.Shared.Extensions;

public static class DirectoryExtension
{
    /// <summary>
    /// Returns the path of the directory which is located in the root folder (where the solution file is).
    /// </summary>
    /// <param name="folderName">Name of the folder.</param>
    /// <returns>Path.</returns>
    public static string GetDirectoryPath(string folderName)
    {
        string current = Directory.GetCurrentDirectory();
        DirectoryInfo info = new(path: current);
        string root = info.GetFiles("*.sln").Length == 0 ? Directory.GetParent(current)!.FullName : current;
        return Path.Combine(path1: root, path2: folderName);
    }
}
