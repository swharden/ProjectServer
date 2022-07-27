namespace ProjectServer.Core;

/// <summary>
/// The cells file is a plain text database with notes about ABF parents in the same folder (cells.txt).
/// This class contains logic for reading/writing cells files and querying their database contents.
/// </summary>
public class CellsFile
{
    public static readonly Dictionary<string, string> KnownColors = GetKnownColors();

    private readonly List<string> Lines = new();

    public CellsFile(string fileContent)
    {
        Lines.AddRange(fileContent.Split("\n").Select(x => x.Trim()));
    }

    private static Dictionary<string, string> GetKnownColors()
    {
        return new Dictionary<string, string>()
        {
            {"", "#FFFFFF"},
            {"?", "#EEEEEE"},
            {"g", "#00FF00"},
            {"g1", "#00CC00"},
            {"g2", "#009900"},
            {"b", "#FF9999"},
            {"i", "#CCCCCC"},
            {"s", "#CCCCFF"},
            {"s1", "#9999DD"},
            {"s2", "#6666BB"},
            {"s3", "#333399"},
            {"w", "#FFFF00"}
        };
    }

    /// <summary>
    /// Return ABF info for the given ABFID if it is present in this cells file
    /// </summary>
    public CellsFileAbf? Lookup(string abfID)
    {
        string heading = string.Empty;

        foreach (string line in Lines)
        {
            if (line.StartsWith("---"))
            {
                heading = line.Substring(3).Trim();
                continue;
            }

            if (line.StartsWith(abfID))
            {
                return ParseLine(line, heading);
            }
        }

        return null;
    }

    /// <summary>
    /// Update information about the given ABFID
    /// </summary>
    public void Update(string abfID, string color, string comment, string[] tags)
    {
        CellsFileAbf abf = new()
        {
            AbfID = abfID,
            Color = color,
            Comment = comment,
            Tags = tags,
        };

        string newLine = CreateLine(abf);

        // if the ABF is already documented replace its line
        for (int i = 0; i < Lines.Count; i++)
        {
            if (Lines[i].StartsWith(abfID))
            {
                Lines[i] = newLine;
                return;
            }
        }

        // if the ABF is not already documented, add it to the bottom of the file
        Lines.Add(newLine);
    }

    /// <summary>
    /// Return ABF info for a line that starts with a valid ABFID
    /// </summary>
    public static CellsFileAbf ParseLine(string line, string heading = "")
    {
        string[] parts = line.Split(" ", 3);

        string abfID = parts[0];

        string color = (parts.Length > 1) ? parts[1].Trim() : string.Empty;
        if (KnownColors.ContainsKey(color))
            color = KnownColors[color];

        string comment = parts.Length > 2 ? parts[2].Trim() : string.Empty;
        if (comment == "?")
            comment = string.Empty;

        List<string> tags = new();
        if (comment.Contains("!TAG:"))
        {
            string[] commentParts = comment.Split("!TAG:");
            tags.AddRange(commentParts.Skip(1).Select(x => x.Trim()));
            comment = commentParts[0].Trim();
        }

        return new CellsFileAbf()
        {
            AbfID = abfID,
            Heading = heading,
            Color = color,
            Comment = comment,
            Tags = tags.ToArray(),
        };
    }

    /// <summary>
    /// Create a cells file line for the given ABF information
    /// </summary>
    public static string CreateLine(CellsFileAbf abf)
    {
        string color = KnownColors.ContainsValue(abf.Color)
            ? KnownColors.FirstOrDefault(x => x.Value == abf.Color).Key
            : abf.Color;

        string comment = string.IsNullOrEmpty(abf.Comment) ? "?" : abf.Comment;

        string commentWithTags = comment + string.Join("", abf.Tags.Select(x => $" !TAG: {x}"));

        return $"{abf.AbfID} {color} {commentWithTags}";
    }

    public string GetText()
    {
        return string.Join("\n", Lines);
    }

    public void WriteFile(string filePath, bool createBackup = false)
    {
        if (createBackup && File.Exists(filePath))
        {
            string backupFilePath = filePath + $".backup.{DateTime.Now.Ticks}.txt";
            System.Diagnostics.Debug.WriteLine($"Creating backup: {backupFilePath}");
            File.Copy(filePath, backupFilePath);
        }

        File.WriteAllText(filePath, GetText());
        System.Diagnostics.Debug.WriteLine($"Saving: {filePath}");
    }
}