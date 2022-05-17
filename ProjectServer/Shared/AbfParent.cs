namespace ProjectServer.Shared;

public class AbfParent
{
    public string AbfFilePath { get; set; } = string.Empty;
    public string AbfID => Path.GetFileNameWithoutExtension(AbfFilePath);
    public string Color { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public string[] Tags { get; set; } = Array.Empty<string>();
    public string[] ChildAbfPaths { get; set; } = Array.Empty<string>();

    public override string ToString()
    {
        return $"Parent {AbfID} ({ChildAbfPaths.Count()} children): " +
            $"Color='{Color}', Comment='{Comment}', Tags={string.Join(",", Tags)}";
    }
}
