namespace BO;

/// <summary>
/// for the tasks list screen
/// </summary>

public class TaskInList
{
    /// <summary>
    /// Id of the task
    /// </summary>
    public int Id { get; init; }
    /// <summary>
    /// Description of the task
    /// </summary>
    public required string Description { get; set; }
    /// <summary>
    /// Name of the task
    /// </summary>
    public required string Name { get; init; }
    /// <summary>
    /// The status of the task (unscheduled, done...)
    /// </summary>
    public BO.Status Status { get; set; }
}
