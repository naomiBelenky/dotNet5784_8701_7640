namespace BO;

/// <summary>
/// a help entity to get info about the task that the engineer is working on
/// </summary>

public class TaskInEngineer
{
    /// <summary>
    /// Id of the task that the engineer is working on
    /// </summary>
    public int Id { get; init; }
    /// <summary>
    /// Name of the task the engineer is working on
    /// </summary>
    public string Name { get; init; }

    public override string ToString() => Tools.ToStringProperty(this);
    //public override string ToString()
    //{
    //    return ($" Id: {Id}, Name: {Name}\n");
    //}
}
