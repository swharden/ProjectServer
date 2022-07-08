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
    }
}
