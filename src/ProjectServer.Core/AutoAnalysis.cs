namespace ProjectServer.Core;

public static class AutoAnalysis
{
    private static string AutoAnalysisFilePath = "X:/Lab Documents/network/autoAnalysisFolders.txt";

    public static void AddFolder(string path)
    {
        File.AppendAllText(AutoAnalysisFilePath, path + "\n");
    }

    public static void RemoveFolder(string path)
    {
        if (!File.Exists(AutoAnalysisFilePath))
            return;

        if (IsWatched(path))
        {
            string[] lines = File.ReadAllLines(AutoAnalysisFilePath);
            string[] lines2 = lines.Where(x => !x.StartsWith(path)).ToArray();
            string txt = string.Join("\n", lines2);
            File.WriteAllText(AutoAnalysisFilePath, txt);
        }
    }

    public static bool IsWatched(string path)
    {
        if (!File.Exists(AutoAnalysisFilePath))
            return false;

        return File.ReadAllLines(AutoAnalysisFilePath)
            .Where(x => x.StartsWith(path))
            .Any();
    }

    public static void SetWatched(string path, bool isWatched)
    {
        if (isWatched)
        {
            AddFolder(path);
        }
        else
        {
            RemoveFolder(path);
        }
    }
}
