namespace BlImplementation;
using BlApi;
using BO;
using System.Xml.Linq;

internal class Bl : IBl
{
    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();

    public IMilestone MileStone => throw new NotImplementedException();

    /// <summary>
    /// the stage of the progect (Planning, MidScheduling, Execution)
    /// </summary>
    public BO.Stage StageOfProject { get => StageOfProject; set => StageOfProject = value; }

   
    public void schedule()
    {
        List<BO.TaskInList> tasks = (Task.ReadAll()).ToList();
        foreach (var task in tasks)
        {
            //BO.Task fullTask = Task.Read(task.Id) ?? throw new BO.BlDoesNotExistException($"Task with ID={task.Id} does not exist");
            
            Console.WriteLine("enter date");
            DateTime date = Convert.ToDateTime(Console.ReadLine());

            if (date < Task.SuggestStartDate(task.Id)) 
                throw new BO.BlForbiddenInThisStage("not valid date, there are later task that this task depend on them");

            Task.UpdateDate(task.Id, date);
        }
    }
}
