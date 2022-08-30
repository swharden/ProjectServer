namespace ProjectServer.Core;

/// <summary>
/// This class contains logic for categorizing ABF files into parent/child relationships.
/// An ABF parent is the first ABF recorded for a cell.
/// An AbfID is the filename of an ABF file (minus the .abf extension).
/// Parent ABF files are those which have another file in the same folder starting with the AbfID.
/// </summary>
public static class Parents
{
    public static Dictionary<string, string[]> GetAbfsByParent(string folderPath)
    {
        string[] filePaths = Directory.GetFiles(folderPath);
        string[] filenames = filePaths.Select(x => Path.GetFileName(x)).ToArray();
        return GetAbfsByParent(folderPath, filenames);
    }

    public static Dictionary<string, string[]> GetAbfsByParent(string folderPath, string[] filenames)
    {
        Dictionary<string, string[]> abfsByParent = new();

        string[] nonAbfFilenames = filenames.Where(x => !x.EndsWith(".abf") && !x.EndsWith(".ignored") && !x.EndsWith(".rsv")).ToArray();

        string[] abfFilenames = filenames.Where(x => x.EndsWith(".abf")).OrderBy(x => x).ToArray();

        // ABFs that come before the first TIF are "orphan" ABF files.
        // Their parent will default to the first ABF file in the folder.
        string parentID = abfFilenames.Any()
            ? Path.GetFileNameWithoutExtension(abfFilenames.First())
            : string.Empty;

        List<string> children = new();

        foreach (string abfFilename in abfFilenames)
        {
            string abfFilePath = Path.Combine(folderPath, abfFilename);
            string abfID = Path.GetFileNameWithoutExtension(abfFilename);
            bool isParent = nonAbfFilenames.Where(x => x.StartsWith(abfID)).Any();

            if (isParent)
            {
                if (children.Any())
                {
                    abfsByParent[parentID] = children.ToArray();
                }
                parentID = abfID;
                children.Clear();
            }

            children.Add(abfFilePath);
        }

        if (children.Any())
        {
            abfsByParent[parentID] = children.ToArray();
        }

        return abfsByParent;
    }
}
