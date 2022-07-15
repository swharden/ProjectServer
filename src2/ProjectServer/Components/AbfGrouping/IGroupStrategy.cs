namespace ProjectServer.Components.AbfGrouping
{
    public interface IGroupStrategy
    {
        public AbfGroup[] GetGroupedAbfs(Domain.AbfParentInfo[] allParents);
    }
}
