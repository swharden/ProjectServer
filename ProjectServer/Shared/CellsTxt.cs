namespace ProjectServer.Shared;

public static class CellsTxt
{
    public static AbfParent[] Read(string folderPath)
    {
        folderPath = Path.GetFullPath(folderPath);
        if (!Directory.Exists(folderPath))
            throw new DirectoryNotFoundException(folderPath);

        string cellsFilePath = Path.Combine(folderPath, "cells.txt");
        if (!File.Exists(cellsFilePath))
            return Array.Empty<AbfParent>();

        List<AbfParent> parents = new();

        string currentGroup = string.Empty;
        foreach (string rawLine in File.ReadAllLines(cellsFilePath))
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
            if (comment == "?")
                comment = string.Empty;

            string group = currentGroup;

            AbfParent parent = new()
            {
                AbfFilePath = Path.Combine(folderPath, abfID + ".abf"),
                Color = color,
                Comment = comment,
                Tags = new string[] { group },
            };

            parents.Add(parent);
        }

        return parents.ToArray();
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
}
