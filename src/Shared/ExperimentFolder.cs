using System.Text.Json;

namespace ProjectServer.Shared;

public class ExperimentFolder
{
    public string FolderPath { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public AbfFolder[] AbfFolders { get; set; } = Array.Empty<AbfFolder>();

    public override string ToString()
    {
        return $"ABF Experiment ({Title}) with {AbfFolders.Length} subfolders and {AbfFolders.Select(x => x.AbfFilePaths.Length).Sum()} ABFs";
    }

    public static ExperimentFolder FromFolderPath(string folderPath, bool scanAbfs)
    {
        folderPath = Path.GetFullPath(folderPath);
        if (!Directory.Exists(folderPath))
            throw new DirectoryNotFoundException(folderPath);

        ExperimentFolder experiment = new()
        {
            FolderPath = folderPath
        };

        string jsonFilePath = Path.Combine(folderPath, "experiment.json");
        if (!File.Exists(jsonFilePath))
            return experiment;

        string json = File.ReadAllText(jsonFilePath);

        DTOs.ExperimentInfo exp = DTOs.ExperimentInfo.FromJson(json);
        experiment.Title = exp.Title;
        experiment.Description = exp.Description;
        experiment.Notes = exp.Notes;

        if (scanAbfs)
            experiment.ScanAbfFolders();

        return experiment;
    }

    public void ScanAbfFolders()
    {
        AbfFolders = Directory.GetDirectories(FolderPath)
            .Select(x => AbfFolder.Scan(x))
            .ToArray();
    }

    public static ExperimentFolder LoadJsonFile(string filePath)
    {
        ExperimentFolder info = new()
        {
            FolderPath = Path.GetDirectoryName(filePath.Replace("\\", "/")) ?? String.Empty,
        };

        string json = File.ReadAllText(filePath);
        using JsonDocument document = JsonDocument.Parse(json);

        if (document.RootElement.TryGetProperty("Title", out JsonElement title))
        {
            info.Title = title.ToString();
        }

        if (document.RootElement.TryGetProperty("Description", out JsonElement description))
        {
            info.Description = description.ToString();
        }

        if (document.RootElement.TryGetProperty("Notes", out JsonElement notes))
        {
            info.Notes = notes.ToString();
        }

        return info;
    }

    public void SaveJsonFile(string filePath)
    {
        using MemoryStream stream = new();
        JsonWriterOptions options = new() { Indented = true };
        using Utf8JsonWriter writer = new(stream, options);

        writer.WriteStartObject();
        writer.WriteString("Title", Title);
        writer.WriteString("Description", Description);
        writer.WriteString("Notes", Notes);
        writer.WriteEndObject();

        writer.Flush();
        string json = System.Text.Encoding.UTF8.GetString(stream.ToArray());

        File.WriteAllText(filePath, json);
    }
}