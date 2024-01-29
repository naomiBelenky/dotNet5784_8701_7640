namespace BO;

/// <summary>
/// a help entity to get info about the engineer that's working on a specific task
/// </summary>

public class EngineerInTask
{
    /// <summary>
    /// Id of the engineer working on the task
    /// </summary>
    public int Id { get; init; }
    /// <summary>
    /// Name of the engineer working on the task
    /// </summary>
    public string Name { get; init; }
}
