namespace BO;

/// <summary>
/// Task entity represents a task with all props.
/// </summary>

public class Task
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Creation {  get; init; }
    public BO.Status? Status { get; set; }
    public List<BO.TaskInList>? Links { get; set; }
    public BO.MilestoneInTask? Milestone { get; set; }
    public DateTime? PlanToStart { get; set; }
    public DateTime? StartWork { get; set; }
    public DateTime? PlanToFinish {  get; set; }
    public DateTime? Deadline {  get; set; }
    public DateTime? FinishDate { get; set;}
    public TimeSpan? Duration { get; set; }
    public string? Product {  get; set; }
    public string? Notes { get; set; }
    public BO.EngineerInTask? Engineer { get; set; }
    public BO.Level Difficulty { get; set; }
}
