using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace AbfMover;

/// <summary>
/// This class is for creating, loading, appending, and saving daily record JSON files
/// </summary>
public class DailyRecord
{
    public readonly List<CellRecord> Cells = new();
    public readonly string Version = "4.0";
    public string Operator = string.Empty;
    public string Animal = string.Empty;
    public string Bath = string.Empty;
    public string Internal = string.Empty;
    public string Drugs = string.Empty;
    public string Notes = string.Empty;
    public int Count => Cells.Count;

    public override string ToString()
    {
        return $"Daily record of {Count} ABFs (Version {Version})";
    }

    public void SaveJson(string filePath)
    {
        using MemoryStream stream = new();
        JsonWriterOptions options = new() { Indented = true };
        using Utf8JsonWriter writer = new(stream, options);

        writer.WriteStartObject();
        writer.WriteString("version", Version);
        writer.WriteString("operator", Operator);
        writer.WriteString("animal", Animal);
        writer.WriteString("bath", Bath);
        writer.WriteString("internal", Internal);
        writer.WriteString("drugs", Drugs);
        writer.WriteString("notes", Notes);
        writer.WriteStartArray("cells");
        foreach (CellRecord cell in Cells)
        {
            writer.WriteStartObject();
            writer.WriteString("id", cell.AbfID);
            writer.WriteString("color", cell.Color);
            writer.WriteString("comment", cell.Comment);
            writer.WriteString("group", cell.Group);
            writer.WriteEndObject();
        }
        writer.WriteEndArray();
        writer.WriteEndObject();

        writer.Flush();
        string json = Encoding.UTF8.GetString(stream.ToArray());
        File.WriteAllText(filePath, json);
    }

    public static DailyRecord FromJson(string filePath)
    {
        using JsonDocument document = JsonDocument.Parse(File.ReadAllText(filePath));

        DailyRecord rec = new();

        string version = document.RootElement.GetProperty("version").GetString()
            ?? throw new InvalidOperationException("JSON element not found: version");
        if (version != rec.Version)
            throw new InvalidOperationException($"JSON version {version} (expect {rec.Version})");

        rec.Operator = document.RootElement.GetProperty("operator").GetString()
            ?? throw new InvalidOperationException("JSON element not found: operator");

        rec.Animal = document.RootElement.GetProperty("animal").GetString()
            ?? throw new InvalidOperationException("JSON element not found: animal");

        rec.Bath = document.RootElement.GetProperty("bath").GetString()
            ?? throw new InvalidOperationException("JSON element not found: bath");

        rec.Internal = document.RootElement.GetProperty("internal").GetString()
            ?? throw new InvalidOperationException("JSON element not found: internal");

        rec.Drugs = document.RootElement.GetProperty("drugs").GetString()
            ?? throw new InvalidOperationException("JSON element not found: drugs");

        rec.Notes = document.RootElement.GetProperty("notes").GetString()
            ?? throw new InvalidOperationException("JSON element not found: notes");

        foreach (JsonElement recipeElement in document.RootElement.GetProperty("cells").EnumerateArray())
        {
            string id = recipeElement.GetProperty("id").GetString()
                ?? throw new InvalidOperationException("JSON element not found: cell id");

            string color = recipeElement.GetProperty("color").GetString()
                ?? throw new InvalidOperationException("JSON element not found: cell color");

            string comment = recipeElement.GetProperty("comment").GetString()
                ?? throw new InvalidOperationException("JSON element not found: cell comment");

            string group = recipeElement.GetProperty("group").GetString()
                ?? throw new InvalidOperationException("JSON element not found: cell group");

            CellRecord cell = new(id, color, comment, group);
            rec.Cells.Add(cell);
        }

        return rec;
    }
}
