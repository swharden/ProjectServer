using System.Text.Json;

namespace ProjectServer.Shared;

// TODO: move these properties into the ExperimentFolder
public class ExperimentFolderInfo
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;

    public static ExperimentFolderInfo LoadJsonFile(string filePath)
    {
        ExperimentFolderInfo info = new();

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
        var options = new JsonSerializerOptions() { WriteIndented = true };
        string json = JsonSerializer.Serialize(this, options);
        File.WriteAllText(filePath, json);
    }
}
