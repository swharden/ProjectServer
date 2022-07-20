using ProjectServer.Domain;

namespace ProjectServer.Components.AbfGrouping
{
    public class GroupByHeader : IGroupStrategy
    {
        public AbfGroup[] GetGroupedAbfs(AbfParentInfo[] allParents)
        {
            List<AbfGroup> groups = new();

            string[] headers = allParents.Select(x => x.Header).Distinct().OrderBy(x => x).ToArray();

            foreach (string header in headers)
            {
                AbfParentInfo[] parents = allParents.Where(x => x.Header == header).ToArray();
                string title = string.IsNullOrWhiteSpace(header) ? "No Header" : header;
                groups.Add(new AbfGroup(title, parents));
            }

            return groups.ToArray();
        }
    }
}
