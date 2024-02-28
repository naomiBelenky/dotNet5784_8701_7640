namespace BlApi;

public interface IBl
{
    //static Stage stage { get; }
    public IEngineer Engineer { get; }
    public ITask Task { get; }
    public IMilestone MileStone { get; }

    public void InitializeDB();

    public void ResetDB();

    public void saveStartDate(DateTime date);


    /// <summary>
    /// return the stage of the progect (Planning or Execution)
    /// </summary>
    /// 

    public BO.Stage getStage();

    /// <summary>
    /// create a schedule automaticly
    /// </summary>
    public void automaticSchedule();

   
}