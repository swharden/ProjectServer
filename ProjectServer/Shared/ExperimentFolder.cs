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

        AbfFolder[] abfFolders = Directory.GetDirectories(folderPath).Select(x => AbfFolder.Scan(x)).ToArray();

        ExperimentFolderInfo info = new() { Title = "Untitled Experiment", Description = "no description" };
        string experimentJsonFilePath = Path.Combine(folderPath, "experiment.json");
        if (File.Exists(experimentJsonFilePath))
        {
            try
            {
                info = ExperimentFolderInfo.LoadJsonFile(experimentJsonFilePath);
            }
            catch (System.Text.Json.JsonException)
            {
                info.Title = "JSON ERROR";
                info.Description = "experiment.json could not be parsed";
            }
        }

        return new ExperimentFolder()
        {
            FolderPath = folderPath,
            AbfFolders = abfFolders,
            Info = info,
        };
    }
}