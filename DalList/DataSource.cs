namespace Dal;

internal static class DataSource
{
    internal static List<DO.Link> Links { get; } = new();
    internal static List<DO.Task> Tasks { get; } = new();
    internal static List<DO.Engineer> Engineers { get; } = new();

    internal static class Config
    {
        //for TaskID
        internal const int TaskId = 1;
        private static int nextTaskID = TaskId;
        internal static int NextTaskID { get => nextTaskID++; }
        //for LinkID
        internal const int LinkId = 1;
        private static int nextLinkID = LinkId;
        internal static int NextLinkID { get => nextLinkID++; }

        //public static DateTime StartProgect { get; set; }
    }

}
