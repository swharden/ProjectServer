namespace ProjectServer.Core;

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

        string[] nonAbfFilenames = filenames.Where(x => !x.EndsWith(".abf") && !x.EndsWith(".ignored")).ToArray();

        string[] incompleteAbfFilenames = filenames.Where(x => x.EndsWith(".rsv"))
            .Select(x => Path.GetFileNameWithoutExtension(x) + ".abf")
            .ToArray();

        string[] abfFilenames = filenames.Where(x => x.EndsWith(".abf"))
            .Where(x => !incompleteAbfFilenames.Contains(x))
            .ToArray();

        string parentID = "orphan";
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
