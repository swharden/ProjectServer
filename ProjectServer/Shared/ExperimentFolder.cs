namespace ProjectServer.Shared;

public class ExperimentFolder
{
    public string FolderPath { get; set; } = string.Empty;
    public ExperimentFolderInfo Info { get; set; } = new ExperimentFolderInfo();
    public AbfFolder[] AbfFolders { get; set; } = Array.Empty<AbfFolder>();

    public override string ToString()
    {
        return $"ABF Experiment ({Info.Title}) with {AbfFolders.Length} subfolders and {AbfFolders.Select(x => x.AbfFilePaths.Length).Sum()} ABFs";
    }

    public static ExperimentFolder Scan(string folderPath)
    {
        folderPath = Path.GetFullPath(folderPath);
        if (!Directory.Exists(folderPath))
            throw new DirectoryNotFoundException(folderPath);

        ExperimentFolderInfo info = new()
        {
            Title = "test title",
            Description = "test description",
            Notes = "test notes",
        };

        return new ExperimentFolder()
        {
            FolderPath = folderPath,
            Info = info,
            AbfFolders = Directory.GetDirectories(folderPath).Select(x => AbfFolder.Scan(x)).ToArray(),
        };
    }
}