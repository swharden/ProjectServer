namespace ProjectServer.Core;

public record CellsFileAbf
{
    public string Heading = string.Empty;
    public string AbfID = string.Empty;
    public string Color = string.Empty;
    public string Comment = string.Empty;
    public string[] Tags = Array.Empty<string>();
}
