namespace AbfMover;

public static class Program
{
    public static void Main(string[] args)
    {
        string[] paths =
        {
            @"C:\Users\swharden\Documents\temp\DailyFolders\CA1",
            @"C:\Users\swharden\Documents\temp\DailyFolders\PFC",
        };

        foreach (string path in paths)
            ProcessFolder(path);
    }

    private static void ProcessFolder(string path, bool dryRun = false)
    {

        string[] filePaths = Directory.GetFiles(path);
        string[] newFolderPaths = filePaths
            .Select(x => GetTargetFolder(x))
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct()
            .ToArray();

        foreach (string folderPath in newFolderPaths)
        {
            Console.WriteLine($"Creating: {folderPath}/");
            if (!dryRun && !Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
        }

        foreach (string filePath in filePaths)
        {
            string targetFolder = GetTargetFolder(filePath);
            if (string.IsNullOrEmpty(targetFolder))
                continue;
            string targetFilePath = Path.Combine(targetFolder, Path.GetFileName(filePath));
            string targetFolderName = Path.GetFileName(targetFolder);
            string fileName = Path.GetFileName(filePath);
            Console.WriteLine($"Moving: {filePath} -> {targetFolderName}/{fileName}");
            if (dryRun)
                continue;
            File.Move(filePath, targetFilePath);
        }
    }

    public static string GetTargetFolder(string abfFilePath)
    {
        string filename = Path.GetFileName(abfFilePath);
        string directoryName = Path.GetDirectoryName(abfFilePath)!;

        string[] extensionsToMove = { ".abf", ".tif", ".sta", ".ignored" };
        bool isFileTypeToMove = extensionsToMove.Contains(Path.GetExtension(filename).ToLower());
        if (!isFileTypeToMove)
            return string.Empty;

        bool firstTwoCharsAreNumeric = int.TryParse(filename.Substring(0, 4), out _);
        if (!firstTwoCharsAreNumeric)
            return string.Empty;

        if (filename.Length == 8)
        {
            string folderName = filename.Substring(0, filename.Length - 7);
            return Path.Combine(directoryName, folderName);
        }
        else if (filename.Split("_").Length > 3)
        {
            string folderName = string.Join("-", filename.Split("_").Take(3));
            return Path.Combine(directoryName, folderName);
        }
        else
        {
            return string.Empty;
        }
    }
}