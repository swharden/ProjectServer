namespace ProjectServer.Core;

public class AbfInfo
{
    public string AbfFilePath { get; set; } = string.Empty;
    public string AbfID => Path.GetFileNameWithoutExtension(AbfFilePath);
    public string Protocol { get; set; } = string.Empty;
    public int SweepCount { get; set; } = 0;
    public double SweepLengthSec { get; set; } = 0;
    public string Comments { get; set; } = string.Empty;
    public bool HasComments => !string.IsNullOrWhiteSpace(Comments);

    public override string ToString()
    {
        return $"{AbfID} ({Protocol}) with {SweepCount} sweeps ({SweepLengthSec} sec each) comments={Comments}";
    }

    public static AbfInfo FromAbf(string abfFilePath)
    {
        abfFilePath = Path.GetFullPath(abfFilePath);
        if (!File.Exists(abfFilePath))
            throw new FileNotFoundException(abfFilePath);

        AbfReader abf = new(abfFilePath);

        return new AbfInfo()
        {
            AbfFilePath = abfFilePath,
            Protocol = abf.Protocol,
            SweepCount = abf.SweepCount,
            SweepLengthSec = abf.SweepLengthSec,
            Comments = abf.Comments,
        };
    }
}
