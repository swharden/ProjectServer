namespace ProjectServer.Domain
{
    /// <summary>
    /// Information about a parent and all of its children.
    /// This class is only used to bundle details together for displaying things on the website.
    /// </summary>
    public class AbfParentInfo
    {
        public string AbfFilePath { get; set; } = string.Empty;
        public string AbfFolder => Path.GetDirectoryName(AbfFilePath) ?? string.Empty;
        public string AnalysisFolder => Path.Combine(AbfFolder, "_autoanalysis");
        public string AbfID => Path.GetFileNameWithoutExtension(AbfFilePath);
        public string Header { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public string[] Tags { get; set; } = Array.Empty<string>();
        public string[] ChildAbfPaths { get; set; } = Array.Empty<string>();
        public int ChildAbfCount => ChildAbfPaths.Length;

        public AbfParentInfo(string path, string header, string color, string comment, string[] tags, string[] children)
        {
            AbfFilePath = path;
            Header = header;
            Color = color;
            Comment = comment;
            Tags = tags;
            ChildAbfPaths = children;
        }

        public string[] GetAnalysisImagePaths()
        {
            if (!Directory.Exists(AnalysisFolder))
                return Array.Empty<string>();

            string[] analysisImagePaths = Directory.GetFiles(AnalysisFolder)
                .Where(x => x.EndsWith(".jpg", StringComparison.InvariantCultureIgnoreCase) || x.EndsWith(".png", StringComparison.InvariantCultureIgnoreCase))
                .ToArray();

            List<string> paths = new();
            foreach (string childAbfPath in ChildAbfPaths)
            {
                string childAbfID = Path.GetFileNameWithoutExtension(childAbfPath);
                paths.AddRange(analysisImagePaths.Where(x => Path.GetFileName(x).StartsWith(childAbfID)));

            }

            return paths.ToArray();
        }

        public void UpdateFromDisk()
        {
            AbfParentInfo[] parents = GetParentsInFolder(AbfFolder);
            AbfParentInfo newParent = parents.Single(x => x.AbfID == AbfID);

            Dictionary<string, string[]> pathsByParent = Core.Parents.GetAbfsByParent(AbfFolder);

            Header = newParent.Header;
            Color = newParent.Color;
            Comment = newParent.Comment;
            Tags = newParent.Tags;
            ChildAbfPaths = pathsByParent[AbfID];
        }

        public static AbfParentInfo[] GetParentsInFolder(string path)
        {
            string cellsFilePath = Path.Combine(path, "cells.txt");
            string cellsTxt = File.Exists(cellsFilePath) ? File.ReadAllText(cellsFilePath) : string.Empty;
            var cells = new Core.CellsFile(cellsTxt);

            List<Domain.AbfParentInfo> parents = new();

            foreach (var kvp in Core.Parents.GetAbfsByParent(path))
            {
                string abfID = kvp.Key;
                string[] childPaths = kvp.Value;

                Domain.AbfParentInfo abfInfo;

                var knownCell = cells.Lookup(abfID);
                if (knownCell is null)
                {
                    abfInfo = new(childPaths.First(), string.Empty, string.Empty, string.Empty, Array.Empty<string>(), childPaths);
                }
                else
                {
                    abfInfo = new(childPaths.First(), knownCell.Heading, knownCell.Color, knownCell.Comment, knownCell.Tags, childPaths);
                }

                parents.Add(abfInfo);
            }

            return parents.ToArray();
        }
    }
}
