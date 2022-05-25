using System.Text.Json;

namespace ProjectServer.Shared;

public class ProjectFolder
{
    public string FolderPath { get; set; } = string.Empty;
    public DateTime Created { get; set; } = DateTime.MinValue;
    public DateTime Modified { get; set; } = DateTime.MinValue;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool Exists => Created > DateTime.MinValue;
    public string Notes { get; set; } = string.Empty;
    public string[] ExperimentFolders { get; set; } = Array.Empty<string>();
    public string[] ExperimentFoldersScanned { get; set; } = Array.Empty<string>();
    public ExperimentFolder[] Experiments { get; set; } = Array.Empty<ExperimentFolder>();

    public static void SeedFolder(string folderPath, string title, string description, string notes = "")
    {
        folderPath = Path.GetFullPath(folderPath);
        if (!Directory.Exists(folderPath))
            throw new DirectoryNotFoundException(folderPath);

        ProjectFolder project = new()
        {
            Created = DateTime.Now,
            Modified = DateTime.Now,
            Title = title,
            Description = description,
            Notes = notes,
        };

        project.SaveJsonFile(Path.Combine(folderPath, "project.json"));
    }

    /// <summary>
    /// Scan experiment folders (including wildcards) and build the list of experiments.
    /// </summary>
    public void ScanAndLoadExperiments()
    {
        ExperimentFoldersScanned = ScanExperimentFolders(ExperimentFolders, FolderPath);
        Experiments = LoadExperiments(ExperimentFoldersScanned);
    }

    /// <summary>
    /// Scan experiment folders provided by the user (which may contain wildcards) 
    /// and return full paths to all directories.
    /// </summary>
    private static string[] ScanExperimentFolders(string[] userProvidedFolderPaths, string projectFolderPath)
    {
        // add default search location for all projects
        userProvidedFolderPaths = userProvidedFolderPaths.Append("experiments/*").Distinct().ToArray();

        List<string> paths = new();

        foreach (string givenFolderPath in userProvidedFolderPaths)
        {
            string absoluteGivenPath = givenFolderPath.Contains(":")
                ? Path.GetFullPath(givenFolderPath)
                : Path.Combine(projectFolderPath, givenFolderPath);

            if (absoluteGivenPath.EndsWith("/*") || absoluteGivenPath.EndsWith("\\*"))
            {
                string givenFolder = absoluteGivenPath.Substring(0, absoluteGivenPath.Length - 2).Replace("\\", "/");
                if (Directory.Exists(givenFolder))
                    paths.AddRange(Directory.GetDirectories(givenFolder));
            }
            else
            {
                paths.Add(absoluteGivenPath);
            }
        }

        return paths.Select(x => x.Replace("/", "\\")).Distinct().ToArray();
    }

    private static ExperimentFolder[] LoadExperiments(string[] experimentFolderPaths)
    {
        List<ExperimentFolder> experiments = new();

        foreach (string folderPath in experimentFolderPaths)
        {
            string experimentJsonFilePath = Path.Combine(folderPath, "experiment.json");

            if (File.Exists(experimentJsonFilePath))
            {
                ExperimentFolder experiment = ExperimentFolder.LoadJsonFile(experimentJsonFilePath);
                experiments.Add(experiment);
            }
        }

        return experiments.ToArray();
    }

    public string GetJson()
    {
        var options = new JsonSerializerOptions() { WriteIndented = true };
        return JsonSerializer.Serialize(this, options);
    }

    public static ProjectFolder Load(string folderPath)
    {
        folderPath = folderPath.Replace("\\", "/");
        folderPath = Path.GetFullPath(folderPath);
        if (!Directory.Exists(folderPath))
            throw new DirectoryNotFoundException(folderPath);

        string jsonFilePath = Path.Combine(folderPath, "project.json");
        ProjectFolder project = LoadJsonFile(jsonFilePath);
        project.FolderPath = project.FolderPath.Replace("/", "\\");
        return project;
    }

    public void Save(string folderPath)
    {
        folderPath = Path.GetFullPath(folderPath);
        if (!Directory.Exists(folderPath))
            throw new DirectoryNotFoundException(folderPath);

        string jsonFilePath = Path.Combine(folderPath, "project.json");
        SaveJsonFile(jsonFilePath);
    }

    public static ProjectFolder LoadJsonFile(string filePath)
    {
        filePath = Path.GetFullPath(filePath);

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            ProjectFolder project = LoadJson(json);
            project.FolderPath = Path.GetFullPath(Path.GetDirectoryName(filePath) ?? string.Empty);
            project.ScanAndLoadExperiments();
            project.FolderPath = Path.GetDirectoryName(filePath) ?? String.Empty;
            return project;
        }
        else
        {
            return new ProjectFolder()
            {
                Created = DateTime.MinValue,
                Modified = DateTime.MinValue,
                FolderPath = Path.GetFullPath(Path.GetDirectoryName(filePath) ?? string.Empty),
            };
        }
    }

    public void SaveJsonFile(string filePath)
    {
        filePath = Path.GetFullPath(filePath);
        FolderPath = Path.GetDirectoryName(filePath) ?? string.Empty;
        Modified = DateTime.Now;
        File.WriteAllText(filePath, GetJson());
    }

    /// <summary>
    /// Load values from potentially incomplete JSON
    /// </summary>
    public static ProjectFolder LoadJson(string json)
    {
        ProjectFolder project = new();

        using JsonDocument document = JsonDocument.Parse(json);

        if (document.RootElement.TryGetProperty("Title", out JsonElement title))
        {
            project.Title = title.ToString();
        }

        if (document.RootElement.TryGetProperty("Description", out JsonElement description))
        {
            project.Description = description.ToString();
        }

        if (document.RootElement.TryGetProperty("Notes", out JsonElement notes))
        {
            project.Notes = notes.ToString();
        }

        if (document.RootElement.TryGetProperty("Created", out JsonElement created))
        {
            project.Created = DateTime.Parse(created.ToString());
        }

        if (document.RootElement.TryGetProperty("Modified", out JsonElement modified))
        {
            project.Modified = DateTime.Parse(modified.ToString());
        }

        if (document.RootElement.TryGetProperty("ExperimentFolders", out JsonElement folders))
        {
            project.ExperimentFolders = folders.EnumerateArray().Select(x => x.ToString()).ToArray();
        }

        return project;
    }
}
