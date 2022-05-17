namespace ProjectServer.Shared;

public class AbfMultiFolderInfo
{
    public string FullPath { get; set; } = string.Empty;
    public string[] SubFolders { get; set; } = Array.Empty<string>();
    public AbfParent[] Parents { get; set; } = Array.Empty<AbfParent>();

    public static AbfMultiFolderInfo FromPath(string path)
    {
        // TODO
        return new AbfMultiFolderInfo();
    }
}
