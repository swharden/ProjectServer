using ProjectServer.Domain;

namespace ProjectServer.Components.AbfGrouping
{
    public class GroupByFolder : IGroupStrategy
    {
        public AbfGroup[] GetGroupedAbfs(AbfParentInfo[] allParents)
        {
            List<AbfGroup> groups = new();

            string[] folderPaths = allParents.Select(x => x.AbfFolder).Distinct().OrderBy(x => x).ToArray();

            foreach (string folderPath in folderPaths)
            {
                string title = Path.GetFileName(folderPath);
                AbfParentInfo[] parents = allParents.Where(x => x.AbfFolder == folderPath).ToArray();
                groups.Add(new AbfGroup(title, parents));
            }

            return groups.ToArray();
        }
    }
}
