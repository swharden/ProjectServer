namespace ProjectServer.Core;

public class AbfReader
{
    public string AbfFilePath { get; private set; } = string.Empty;
    public string AbfID => Path.GetFileNameWithoutExtension(AbfFilePath);
    public int SweepCount { get; private set; } = -1;
    public double SweepLengthSec { get; private set; } = -1;
    public string Protocol { get; private set; } = string.Empty;
    public string Comments { get; private set; } = string.Empty;

    public AbfReader(string abfPath)
    {
        abfPath = Path.GetFullPath(abfPath);
        if (!File.Exists(abfPath))
            throw new FileNotFoundException(abfPath);
        AbfFilePath = abfPath;

        using FileStream fs = File.OpenRead(abfPath);
        using BinaryReader reader = new(fs);

        string magicString = System.Text.Encoding.UTF8.GetString(reader.ReadBytes(4));

        if (magicString == "ABF ")
        {
            ReadABF1(reader);
        }
        else if (magicString == "ABF2")
        {
            ReadABF2(reader);
        }
        else
        {
            Comments = $"invalid ABF file: {magicString}";
        }
    }

    public void ReadABF1(BinaryReader reader)
    {
        Comments = "ABF1 files not yet supported"; // TODO: support ABF1
    }

    public void ReadABF2(BinaryReader reader)
    {
        // get section locations

        reader.BaseStream.Seek(76, SeekOrigin.Begin);
        int protocolSection_firstByte = (int)reader.ReadUInt32() * 512;
        int protocolSection_size = (int)reader.ReadUInt32();
        int protocolSection_count = (int)reader.ReadUInt32();

        reader.BaseStream.Seek(92, SeekOrigin.Begin);
        int adcSection_firstByte = (int)reader.ReadUInt32() * 512;
        int adcSection_size = (int)reader.ReadUInt32();
        int adcSection_count = (int)reader.ReadUInt32();

        reader.BaseStream.Seek(220, SeekOrigin.Begin);
        int stringsSection_firstByte = (int)reader.ReadUInt32() * 512;
        int stringsSection_size = (int)reader.ReadUInt32();
        int stringsSection_count = (int)reader.ReadUInt32();

        reader.BaseStream.Seek(236, SeekOrigin.Begin);
        int dataSection_firstByte = (int)reader.ReadUInt32() * 512;
        int dataSection_size = (int)reader.ReadUInt32();
        int dataSection_count = (int)reader.ReadUInt32();

        // read useful values from fixed offsets relative to certain sections
        reader.BaseStream.Seek(protocolSection_firstByte, SeekOrigin.Begin);
        int nOperationMode = reader.ReadUInt16();
        bool gapFree = (nOperationMode == 3);

        float fADCSequenceInterval = reader.ReadSingle();
        int sampleRate = (int)(1e6 / fADCSequenceInterval);

        reader.BaseStream.Seek(12, SeekOrigin.Begin);
        SweepCount = (int)reader.ReadUInt32();
        if (gapFree)
            SweepCount = 1;
        int channelCount = adcSection_count;
        SweepLengthSec = (double)dataSection_count / SweepCount / channelCount / sampleRate;

        // get protocol from strings
        reader.BaseStream.Seek(stringsSection_firstByte + 44, SeekOrigin.Begin);
        while (reader.ReadByte() == 0) { };
        reader.BaseStream.Seek(-1, SeekOrigin.Current);

        System.Text.StringBuilder sb = new();
        sb.Append(" \n");
        char last = '\0';
        for (int i = 0; i < stringsSection_size; i++)
        {
            char c = reader.ReadChar();
            if (c == '\0')
            {
                sb.Append('\n');
                if (last == '\0')
                    break;
            }
            else
            {
                sb.Append(c);
            }
            last = c;
        }

        string[] strings = sb.ToString().Split('\n');
        for (int i = 0; i < strings.Length; i++)
            strings[i].Replace("\0", "").Trim();

        reader.BaseStream.Seek(72, SeekOrigin.Begin);
        int uProtocolPathIndex = (int)reader.ReadUInt32();
        string protocolPath = strings[uProtocolPathIndex]; // WARNING: may contain backslashes
        string safeProtocolPath = protocolPath.Replace("\\", "/");
        Protocol = Path.GetFileNameWithoutExtension(safeProtocolPath);

        // read comment tags
        reader.BaseStream.Seek(protocolSection_firstByte + 14, SeekOrigin.Begin);
        float fSynchTimeUnit = reader.ReadSingle();
        int commentTagType = 1;

        List<string> tags = new();

        reader.BaseStream.Seek(252, SeekOrigin.Begin);
        int tagSectionFirstByte = (int)reader.ReadUInt32() * 512;
        int tagSectionSize = (int)reader.ReadUInt32();
        int tagSectionCount = (int)reader.ReadUInt32();

        for (int i = 0; i < tagSectionCount; i++)
        {
            int tagByte = tagSectionFirstByte + i * tagSectionSize;
            reader.BaseStream.Seek(tagByte, SeekOrigin.Begin);
            int lTagTime = (int)reader.ReadUInt32();
            double tagTimeSec = lTagTime * fSynchTimeUnit / 1e6;
            double tagTimeMin = Math.Round(tagTimeSec / 60, 2);
            string sTagComment = System.Text.Encoding.UTF8.GetString(reader.ReadBytes(56));
            int nTagType = reader.ReadUInt16();
            if (nTagType == commentTagType)
            {
                string tagComment = sTagComment.Trim();
                tags.Add($"{tagComment} @ {tagTimeMin} min");
            }
            else
            {
                tags.Add($"tag @ {tagTimeMin} min");
            }
        }

        Comments = string.Join(", ", tags);
    }
}
