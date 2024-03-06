namespace BlApi;

public interface IBl
{
    //static Stage stage { get; }
    public IEngineer Engineer { get; }
    public ITask Task { get; }
    public IMilestone MileStone { get; }

    public DateTime Clock { get; }

    /// <summary>
    /// initialize the time
    /// </summary>

    public DateTime InitTime();

    /// <summary>
    /// promote the hours of the time
    /// </summary>
    public void PromoteHour();

    /// <summary>
    /// promote the minutes of the time
    /// </summary>
    public void PromoteDay();

    /// <summary>
    /// promote the seconds of the time
    /// </summary>
    public void PromoteYear();

    


    public void InitializeDB();

    public void ResetDB();

    public void saveStartDate(DateTime date);

    public DateTime? getStartDate();



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