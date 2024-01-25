namespace BO;

/// <summary>
/// Engineer Entity represents an engineer with all its props
/// </summary>
/// <param name="Id"> Personal unique ID of the engineer </param>
/// <param name="Name"> The engineer's name </param>
/// <param name="Email"> The engineer's email </param>
/// <param name="Level"> The engineer's level </param>
/// <param name="Cost"> The engineer's cost per hour </param>
/// <param name="Task"> The task the engineer is working on </param>

public class Engineer
{
    public int Id { get; init; }
    public string Name { get; set; }
    public string Email { get; set; }
    public EngineerLevel Level { get; set; }
    public double Cost { get; set; }
    public BO.TaskInEngineer? Task {  get; set; }
}
