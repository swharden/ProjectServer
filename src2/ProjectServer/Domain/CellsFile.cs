﻿namespace ProjectServer.Domain
{
    public static class CellsFile
    {
        public static AbfParentInfo[] Load(string folderPath)
        {
            folderPath = Path.GetFullPath(folderPath);
            if (!Directory.Exists(folderPath))
                throw new DirectoryNotFoundException(folderPath);

            string cellsFilePath = Path.Combine(folderPath, "cells.txt");
            if (!File.Exists(cellsFilePath))
                return Array.Empty<AbfParentInfo>();

            Dictionary<string, string[]> abfsByParent = Parents.GetAbfsByParent(folderPath);

            List<AbfParentInfo> parents = new();

            string currentHeader = string.Empty;
            foreach (string rawLine in File.ReadAllLines(cellsFilePath))
            {
                string line = rawLine.Trim();

                if (line.StartsWith("#"))
                    continue;

                if (line.Length == 0)
                    continue;

                if (line.StartsWith("---"))
                {
                    currentHeader = line.Substring(3).Trim();
                    continue;
                }

                string[] parts = line.Split(" ", 3);
                string abfID = parts[0];
                string color = GetHexColor(parts.Length > 1 ? parts[1] : string.Empty);
                string comment = parts.Length > 2 ? parts[2] : string.Empty;
                if (comment == "?")
                    comment = string.Empty;
                string[] tags = Array.Empty<string>(); // TODO: extract from comments
                string abfPath = Path.Combine(folderPath, abfID + ".abf");
                string[] children = abfsByParent.ContainsKey(abfID) ? abfsByParent[abfID] : Array.Empty<string>();

                AbfParentInfo parent = new(abfPath, currentHeader, color, comment, tags, children);

                parents.Add(parent);
            }

            return parents.ToArray();
        }

        public static string[] GetDefaultColors()
        {
            return new string[]
            {
            "#FFFFFF",
            "#EEEEEE",
            "#00FF00",
            "#00CC00",
            "#009900",
            "#FF9999",
            "#CCCCCC",
            "#CCCCFF",
            "#9999DD",
            "#6666BB",
            "#333399",
            "#FFFF00",
            };
        }

        public static string GetHexColor(string code)
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
}