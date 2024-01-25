namespace BO;

public class MilestoneInList
{
    public string Description { get; set; }
    public string Name { get; init; }
    public DateTime creation { get; init; }
    public BO.Status status { get; set; }
    public double CompletionPrecentage { get; set; }
}
