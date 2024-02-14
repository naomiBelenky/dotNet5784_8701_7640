namespace BlApi;

public interface IBl
{
    //static Stage stage { get; }
    public IEngineer Engineer { get; }
    public ITask Task { get; }
    public IMilestone MileStone { get; }

    public void InitializeDB();

    /// <summary>
    /// return the stage of the progect (Planning or Execution)
    /// </summary>
    /// 

    public BO.Stage getStage();
    public void automaticSchedule();
}