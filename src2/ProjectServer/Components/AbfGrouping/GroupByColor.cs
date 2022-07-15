using ProjectServer.Domain;

namespace ProjectServer.Components.AbfGrouping
{
    public class GroupByColor : IGroupStrategy
    {
        public AbfGroup[] GetGroupedAbfs(AbfParentInfo[] allParents)
        {
            List<AbfGroup> groups = new();

            string[] colors = allParents.Select(x => x.Color).Distinct().OrderBy(x => x).ToArray();

            foreach (string color in colors)
            {
                AbfParentInfo[] parents = allParents.Where(x => x.Color == color).ToArray();
                groups.Add(new AbfGroup(color, parents));
            }

            return groups.ToArray();
        }
    }
}
