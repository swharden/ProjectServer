using ProjectServer.Domain;

namespace ProjectServer.Components.AbfGrouping;

public class AbfGroup
{
    public string Title { get; set; } = string.Empty;
    public string[] NotesLines { get; set; } = Array.Empty<string>();
    public string[] OriginLines { get; set; } = Array.Empty<string>();
    public AbfParentInfo[] Parents { get; set; } = Array.Empty<AbfParentInfo>();

    public AbfGroup(string title, AbfParentInfo[] parents)
    {
        Title = title;
        Parents = parents;
        OriginLines = parents.Select(x => $"setpath \"{x.AbfFilePath}\"").ToArray();

        // only load notes if all parents are in the same folder
        if (parents.Select(x => x.AbfFolder).Distinct().Count() == 1)
        {
            NotesLines = Core.AbfExperimentFile.FromFolder(parents.First().AbfFolder).GetText().Split("\n");
        }
    }
}
