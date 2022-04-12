namespace AbfMover;

/// <summary>
/// Methods for interacting with old-format cells.txt files and folders
/// </summary>
public static class CellsTxt
{
    public static CellRecord[] GetCellRecords(string filePath)
    {
        List<CellRecord> cells = new();

        if (!File.Exists(filePath))
            throw new FileNotFoundException(filePath);

        string currentGroup = string.Empty;
        foreach (string rawLine in File.ReadAllLines(filePath))
        {
            string line = rawLine.Trim();

            if (line.StartsWith("#"))
                continue;

            if (line.Length == 0)
                continue;

            if (line.StartsWith("---"))
            {
                currentGroup = line.Substring(3).Trim();
                continue;
            }

            string[] parts = line.Split(" ", 3);
            string abfID = parts[0];
            string color = GetColor(parts.Length > 1 ? parts[1] : string.Empty);
            string comment = parts.Length > 2 ? parts[2] : string.Empty;
            string group = currentGroup;

            CellRecord cell = new(abfID, color, comment, group);
            cells.Add(cell);
        }

        return cells.ToArray();
    }

    public static string GetSubfolderName(string abfID)
    {
        if (abfID.Contains('.'))
            throw new InvalidDataException($"abfID must not contain a decimal or extension: {abfID}");

        if (abfID.Length == 8)
            return abfID.Substring(0, 5);

        if (abfID.Split("_").Length > 3)
            return string.Join("-", abfID.Split("_").Take(3));

        throw new ArgumentException($"cannot determine subfolder name for abfid: {abfID}");
    }

    private static string GetColor(string code)
    {
        return code switch
        {
            "" => string.Empty,
            "?" => "#EEEEEE",
            "g" => "#00FF00",
            "g1" => "#00CC00",
            "g2" => "#009900",
            "b" => "#FF9999",
            "i" => "#CCCCCC",
            "s" => "#CCCCFF",
            "s1" => "#9999DD",
            "s2" => "#6666BB",
            "s3" => "#333399",
            "w" => "#FFFF00",
            _ => string.Empty,
        };
    }

    /// <summary>
    /// Break a folder with numerous ABFs into a series of smaller folders organized by date.
    /// The original cells.txt will be broken into individual abf-day.json files in each subfolder.
    /// </summary>
    public static void BreakUp(string topFolder)
    {
        BreakUpMoveFiles(topFolder);
        BreakUpCellsFile(topFolder);
    }

    private static void BreakUpMoveFiles(string topFolder)
    {
        foreach (string filePath in Directory.GetFiles(topFolder))
        {
            try
            {
                string baseName = Path.GetFileNameWithoutExtension(filePath).Split(".")[0];
                string dayName = CellsTxt.GetSubfolderName(baseName);
                Console.WriteLine($"[{dayName}] {filePath}");
                string dayFolder = Path.Combine(topFolder, dayName);
                if (!Directory.Exists(dayFolder))
                    Directory.CreateDirectory(dayFolder);
                string filePath2 = Path.Combine(dayFolder, Path.GetFileName(filePath));
                File.Move(filePath, filePath2);
            }
            catch (ArgumentException)
            {
                Console.WriteLine($"SKIPPING: {filePath}");
                continue;
            }

        }
    }

    private static void BreakUpCellsFile(string topFolder)
    {
        string cellsFilePath = Path.Combine(topFolder, "cells.txt");
        if (!File.Exists(cellsFilePath))
            return;

        CellRecord[] cells = GetCellRecords(cellsFilePath);
        Dictionary<string, CellRecord[]> cellsByDay = cells
            .GroupBy(x => GetSubfolderName(x.AbfID))
            .ToDictionary(x => x.Key, x => x.ToArray());

        foreach (string dayName in cellsByDay.Keys)
        {
            string dayFolder = Path.Combine(topFolder, dayName);
            if (!Directory.Exists(dayFolder))
                Directory.CreateDirectory(dayFolder);

            string dailyRecordFilePath = Path.Combine(dayFolder, "abf-day.json");
            if (File.Exists(dailyRecordFilePath))
                throw new InvalidDataException($"already exists: {dailyRecordFilePath}");

            DailyRecord dayRecord = new();
            dayRecord.Cells.AddRange(cellsByDay[dayName]);
            dayRecord.SaveJson(dailyRecordFilePath);
            Console.WriteLine(dailyRecordFilePath);
        }

        File.Move(cellsFilePath, cellsFilePath + ".backup");
    }
}
