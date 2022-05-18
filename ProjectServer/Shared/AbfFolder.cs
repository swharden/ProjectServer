namespace ProjectServer.Shared;

/// <summary>
/// Contains information about a single folder containing ABFs.
/// Static methods are tools for working with single ABF folders.
/// </summary>
public class AbfFolder
{
    public string FolderPath { get; set; } = string.Empty;
    public string ExperimentTxt { get; set; } = string.Empty;
    public string[] AbfFilePaths { get; set; } = Array.Empty<string>();
    public string[] AnalysisFilePaths { get; set; } = Array.Empty<string>();
    public AbfParent[] AbfParents { get; set; } = Array.Empty<AbfParent>();

    public AbfFolder()
    {

    }

    public override string ToString()
    {
        return $"ABF Folder with {AbfFilePaths.Length} ABFs ({AbfParents.Length} parents) and {AnalysisFilePaths.Length} analysis files";
    }

    public static AbfFolder Scan(string folderPath)
    {
        folderPath = Path.GetFullPath(folderPath);
        if (!Directory.Exists(folderPath))
            throw new DirectoryNotFoundException(folderPath);

        string[] allFilePaths = Directory.GetFiles(folderPath, "*.*");
        string[] allFileNames = allFilePaths.Select(x => Path.GetFileName(x)).ToArray();
        string[] abfFilePaths = allFilePaths.Where(x => x.EndsWith(".abf")).ToArray();

        string autoAnalysisFolderName = "_autoanalysis";
        string autoAnalysisFolderPath = Path.Combine(folderPath, autoAnalysisFolderName);
        string[] analysisFilePaths = Directory.Exists(autoAnalysisFolderPath)
            ? Directory.GetFiles(autoAnalysisFolderPath, "*.*")
            : Array.Empty<string>();

        string experimentFilePath = Path.Combine(folderPath, "experiment.txt");
        string experimentTxt = File.Exists(experimentFilePath)
            ? File.ReadAllText(experimentFilePath)
            : string.Empty;

        AbfParent[] parents = GetParentsFromFileList(folderPath, allFileNames, analysisFilePaths);
        AddNotesFromCellsTxt(parents, folderPath);

        return new AbfFolder()
        {
            FolderPath = folderPath,
            ExperimentTxt = experimentTxt,
            AbfFilePaths = abfFilePaths,
            AnalysisFilePaths = analysisFilePaths,
            AbfParents = parents,
        };
    }

    private static AbfParent[] GetParentsFromFileList(string folderPath, string[] filenames, string[] analysisFilePaths)
    {
        string[] nonAbfFilenames = filenames.Where(x => !x.EndsWith(".abf") && !x.EndsWith(".ignored")).ToArray();
        string[] abfFilenames = filenames.Where(x => x.EndsWith(".abf")).ToArray();

        List<AbfParent> parents = new()
        {
            new AbfParent() { AbfFilePath = "orphan" },
        };

        foreach (string abfFilename in abfFilenames)
        {
            string abfFilePath = Path.Combine(folderPath, abfFilename);
            string abfID = Path.GetFileNameWithoutExtension(abfFilename);
            bool isParent = nonAbfFilenames.Where(x => x.StartsWith(abfID)).Any();

            if (isParent)
            {
                AbfParent newParent = new() { AbfFilePath = abfFilename };
                parents.Add(newParent);
            }

            parents.Last().ChildAbfPaths = parents.Last().ChildAbfPaths.Append(abfFilePath).ToArray();
        }

        foreach (AbfParent parent in parents)
        {
            List<string> childImagePaths = new();

            foreach (string abfid in parent.ChildAbfPaths.Select(x => Path.GetFileNameWithoutExtension(x)))
                childImagePaths.AddRange(analysisFilePaths.Where(x => Path.GetFileName(x).StartsWith(abfid)));

            parent.ChildImagePaths = childImagePaths.ToArray();
        }

        return parents.Where(x => x.ChildAbfPaths.Any()).ToArray();
    }

    private static AbfParent[] AddNotesFromCellsTxt(AbfParent[] parents, string abfFolderPath)
    {
        AbfParent[] cellsParents = CellsTxt.Read(abfFolderPath);

        // TODO: reduce big O complexity using a hashmap
        foreach (AbfParent cellsParent in cellsParents)
        {
            foreach (AbfParent parent in parents)
            {
                if (parent.AbfID == cellsParent.AbfID)
                {
                    parent.Color = cellsParent.Color;
                    parent.Comment = cellsParent.Comment;
                    parent.Tags = cellsParent.Tags;
                }
            }
        }

        return parents;
    }
}
