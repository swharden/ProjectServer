namespace ProjectServer.Core;

/// <summary>
/// This POCO contains information about an experiment.json file
/// </summary>
public class ExperimentNotes
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string NotesTxt { get; set; } = string.Empty;
    public string NotesMd { get; set; } = string.Empty;

    public static ExperimentNotes FromJson(string json)
    {
        using System.Text.Json.JsonDocument document = System.Text.Json.JsonDocument.Parse(json);

        string? title = document.RootElement.GetProperty("title").GetString();
        string? description = document.RootElement.GetProperty("description").GetString();
        string? notesText = document.RootElement.GetProperty("notes-text").GetString();
        string? notesMarkdown = document.RootElement.GetProperty("notes-markdown").GetString();

        return new ExperimentNotes()
        {
            Title = title ?? string.Empty,
            Description = description ?? string.Empty,
            NotesTxt = notesText ?? string.Empty,
            NotesMd = notesMarkdown ?? string.Empty,
        };
    }

    public string ToJson()
    {
        using MemoryStream stream = new();
        System.Text.Json.JsonWriterOptions options = new() { Indented = true };
        using System.Text.Json.Utf8JsonWriter writer = new(stream, options);

        writer.WriteStartObject();
        writer.WriteString("title", Title);
        writer.WriteString("description", Description);
        writer.WriteString("notes-text", NotesTxt);
        writer.WriteString("notes-markdown", NotesMd);
        writer.WriteEndObject();

        writer.Flush();
        string json = System.Text.Encoding.UTF8.GetString(stream.ToArray());

        return json;
    }
}
