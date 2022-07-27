namespace ProjectServer.Core;

/// <summary>
/// This POCO is used to pass information about an ABF Parent that was annotated using a cells file.
/// </summary>
public record CellsFileAbf
{
    public string Heading = string.Empty;
    public string AbfID = string.Empty;
    public string Color = string.Empty;
    public string Comment = string.Empty;
    public string[] Tags = Array.Empty<string>();
}
