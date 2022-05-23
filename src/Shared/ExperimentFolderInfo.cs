using System.Text.Json;

namespace ProjectServer.Shared;

public class ExperimentFolderInfo
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;

    public static ExperimentFolderInfo LoadJsonFile(string filePath)
    {
        return JsonSerializer.Deserialize<ExperimentFolderInfo>(File.ReadAllText(filePath))
            ?? throw new NullReferenceException();
    }

    public void SaveJsonFile(string filePath)
    {
        var options = new JsonSerializerOptions() { WriteIndented = true };
        string json = JsonSerializer.Serialize(this, options);
        File.WriteAllText(filePath, json);
    }
}
