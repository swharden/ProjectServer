namespace ProjectServer.Core;

/// <summary>
/// This class is a POCO used to pass around basic information about ABF files.
/// </summary>
public class AbfInfo
{
    public string AbfFilePath { get; set; } = string.Empty;
    public string AbfID => Path.GetFileNameWithoutExtension(AbfFilePath);
    public string Protocol { get; set; } = string.Empty;
    public int SweepCount { get; set; } = 0;
    public double SweepLengthSec { get; set; } = 0;
    public string Comments { get; set; } = string.Empty;
    public bool HasComments => !string.IsNullOrWhiteSpace(Comments);

    public bool IsValid => SweepCount > 0;
    public bool IsLocked => Comments.StartsWith("The process cannot access the file");

    public override string ToString()
    {
        return $"{AbfID} ({Protocol}) with {SweepCount} sweeps ({SweepLengthSec} sec each) comments={Comments}";
    }

    public static AbfInfo FromAbf(string abfFilePath)
    {
        abfFilePath = Path.GetFullPath(abfFilePath);
        if (!File.Exists(abfFilePath))
            throw new FileNotFoundException(abfFilePath);

        try
        {
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
        catch (Exception ex)
        {
            return new AbfInfo()
            {
                AbfFilePath = abfFilePath,
                Protocol = string.Empty,
                SweepCount = -1,
                SweepLengthSec = -1,
                Comments = ex.Message,
            };
        }
    }
}
