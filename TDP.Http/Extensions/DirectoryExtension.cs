using TDP.Http.Helpers;

namespace System.IO;

public static class DirectoryExtension
{
    public static string GetDirectoryPath(string folderName)
    {
        string current = Directory.GetCurrentDirectory();
        string root = Directory.GetParent(current)!.FullName;
        return Path.Combine(path1: root, path2: folderName);
    }
}
