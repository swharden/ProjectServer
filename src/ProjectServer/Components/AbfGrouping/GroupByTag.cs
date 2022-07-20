using ProjectServer.Domain;

namespace ProjectServer.Components.AbfGrouping
{
    public class GroupByTag : IGroupStrategy
    {
        public AbfGroup[] GetGroupedAbfs(AbfParentInfo[] allParents)
        {
            List<AbfGroup> groups = new();

            string[] tags = allParents.SelectMany(x => x.Tags).Distinct().OrderBy(x => x).ToArray();

            AbfParentInfo[] parentsWithoutTags = allParents.Where(x => !x.Tags.Any()).ToArray();
            groups.Add(new AbfGroup("no tag", parentsWithoutTags));

            foreach (string tag in tags)
            {
                AbfParentInfo[] parents = allParents.Where(x => x.Tags.Contains(tag)).ToArray();
                groups.Add(new AbfGroup(tag, parents));
            }

            return groups.ToArray();
        }
    }
}
