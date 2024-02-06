namespace BlImplementation;
using BlApi;
using System.Xml.Linq;

internal class Bl : IBl
{
    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();

    public IMilestone MileStone => throw new NotImplementedException(); 
    /// <summary>
    /// the stage of the progect (Planning, MidScheduling, Execution)
    /// </summary>
    public BO.Stage Stage { get; set; }

    public void schedule()
    {
        List<BO.TaskInList> tasks = (Task.ReadAll()).ToList();
        foreach (var task in tasks)
        {
            BO.Task fullTask = Task.Read(task.Id) ?? throw new BO.BlDoesNotExistException($"Task with ID={task.Id} does not exist");
            DateTime what = Task.SuggestStartDate(fullTask.Id);
        }
    }
}
