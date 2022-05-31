using System.Text;
using System.Text.Json;

namespace ProjectServer.Shared.DTOs;

/// <summary>
/// This DTO conveys project information between client/server using get/put HTTP requests.
/// The only logic that belongs in this module is that required to convert to/from text file format.
/// </summary>
public class ProjectInfo
{
    public string FolderPath { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string[] ExperimentFolders { get; set; } = Array.Empty<string>();

    public string ToJson()
    {
        using MemoryStream stream = new();
        JsonWriterOptions options = new() { Indented = true };
        using Utf8JsonWriter writer = new(stream, options);

        writer.WriteStartObject();
        writer.WriteString("Title", Title);
        writer.WriteString("Description", Description);
        writer.WriteString("Notes", Notes);
        writer.WriteStartArray("ExperimentFolders");
        foreach (string folder in ExperimentFolders)
        {
            writer.WriteStringValue(folder);
        }
        writer.WriteEndArray();
        writer.WriteEndObject();

        writer.Flush();
        string json = Encoding.UTF8.GetString(stream.ToArray());

        return json;
    }

    public static ProjectInfo FromJson(string json)
    {
        using JsonDocument document = JsonDocument.Parse(json);

        ProjectInfo project = new();

        if (document.RootElement.TryGetProperty("Title", out JsonElement title))
            project.Title = title.ToString();

        if (document.RootElement.TryGetProperty("Description", out JsonElement description))
            project.Description = description.ToString();

        if (document.RootElement.TryGetProperty("Notes", out JsonElement notes))
            project.Notes = notes.ToString();

        if (document.RootElement.TryGetProperty("ExperimentFolders", out JsonElement folders))
            project.ExperimentFolders = folders.EnumerateArray().Select(x => x.ToString()).ToArray();

        return project;
    }
}