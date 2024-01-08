namespace DO;

/// <summary>
/// links between one task the the task it depends on.
/// </summary>
/// <param name="LinkID">Unique ID for the Link</param>
/// <param name="PrevTask">The next task has to wait for this task to be done</param>
/// <param name="NextTask">This task has to wait for the previous task to be done</param>
public record Link
(
    int LinkID,
    int PrevTask,
    int NextTask
)
{
    public Link() : this(0, 0, 0) { }
}
