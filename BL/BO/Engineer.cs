namespace BO;

/// <summary>
/// Engineer Entity represents an engineer with all its props
/// </summary>

public class Engineer
{
    /// <summary>
    /// Personal unique ID of the engineer
    /// </summary>
    public int Id { get; init; }
    /// <summary>
    /// The engineer's name
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// The engineer's email
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// The engineer's level
    /// </summary>
    public Level Level { get; set; }
    /// <summary>
    /// The engineer's cost per hour
    /// </summary>
    public double Cost { get; set; }
    /// <summary>
    /// The task the engineer is working on
    /// </summary>
    public BO.TaskInEngineer? Task {  get; set; }
}
