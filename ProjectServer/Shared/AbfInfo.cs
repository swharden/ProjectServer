namespace ProjectServer.Shared;

public class AbfInfo
{
    public string AbfFilePath { get; set; } = string.Empty;
    public string AbfID => Path.GetFileNameWithoutExtension(AbfFilePath);
    public string Protocol { get; set; } = string.Empty;
    public int SweepCount { get; set; } = 0;
    public double SweepLengthSec { get; set; } = 0;
    public string Comments { get; set; } = string.Empty;

    public static AbfInfo FromAbf(string abfFilePath)
    {
        abfFilePath = Path.GetFullPath(abfFilePath);
        if (!File.Exists(abfFilePath))
            throw new FileNotFoundException(abfFilePath);

        return new AbfInfo()
        {
            AbfFilePath = abfFilePath,
            Protocol = "test protocol",
            SweepCount = 420,
            SweepLengthSec = 69,
            Comments = "test comments",
        };
    }
}
