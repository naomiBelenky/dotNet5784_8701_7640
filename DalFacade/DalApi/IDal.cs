namespace DalApi;

public interface IDal
{
    ITask Task { get; }
    IEngineer Engineer { get; }
    ILink Link { get; }
    public DateTime? StartDate { get; set; }
    DateTime? FinishDate { get; set; }
}
