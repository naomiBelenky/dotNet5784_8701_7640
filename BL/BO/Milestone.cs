namespace BO;

/// <summary>
/// Milestone entity 
/// </summary>

public class Milestone
{
    public int Id { get; init; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Creation { get; set; }
    public BO.Status Status { get; set; }
    public DateTime StartWork { get; set; }
    public DateTime? PlanToFinish { get; set; }
    public DateTime? Deadline { get; set; }
    public DateTime? FinishDate { get; set; }
    public double CompletionPrecentage { get; set; }
    public string? Notes { get; set; }
    public List<BO.MilestoneInList>? Links { get; set; }
}
