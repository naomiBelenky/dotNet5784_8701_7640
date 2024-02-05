namespace BlApi;

public interface IBl
{
    //static Stage stage { get; }
    public IEngineer Engineer { get; }
    public ITask Task { get; }
    public IMilestone MileStone { get; }
}