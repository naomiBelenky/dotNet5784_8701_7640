namespace Dal;

internal static class DataSource
{
    internal static List<DO.Link> Links { get; } = new();
    internal static List<DO.Task> Tasks { get; } = new();
    internal static List<DO.Engineer> Engineers { get; } = new();

    internal static class Config
    {
        internal const int LinkID = 1;
        private static int nextLinkID = LinkID;
        internal static int NextLinkID { get => nextLinkID++; }

        internal const int TaskID = 1;
        private static int nextTaskID = LinkID;
        internal static int NextTaskID { get => nextTaskID++; }
    }

}
