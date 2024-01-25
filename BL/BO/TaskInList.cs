namespace BO;

public class TaskInList
{
    public int Id { get; init; }
    public required string Description { get; set; }
    public required string Name { get; init; }
    public BO.Status Status { get; set; }
}
