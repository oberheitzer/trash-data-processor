namespace System.IO.Abstractions;

public static class FileSystemExtensions
{
    /// <summary>
    /// Returns the path of the directory which is located in the root folder (where the solution file is).
    /// </summary>
    /// <param name="fileSystem">File system abstraction.</param>
    /// <param name="folderName">Name of the folder.</param>
    /// <returns>Path.</returns>
    public static string GetDirectoryPath(this IFileSystem fileSystem, string folderName)
    {
        string current = fileSystem.Directory.GetCurrentDirectory();
        var info = fileSystem.DirectoryInfo.New(path: current);
        string root = info.GetFiles("*.sln").Length == 0 ? fileSystem.Directory.GetParent(current)!.FullName : current;
        return fileSystem.Path.Combine(path1: root, path2: folderName);
    }
}