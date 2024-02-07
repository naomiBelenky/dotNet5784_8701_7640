namespace BlApi;

public interface IBl
{
    //static Stage stage { get; }
    public IEngineer Engineer { get; }
    public ITask Task { get; }
    public IMilestone MileStone { get; }

    /// <summary>
    /// the stage of the progect (Planning, MidScheduling, Execution)
    /// </summary>
    public BO.Stage StageOfProject { get; set; }
    public void schedule();
}