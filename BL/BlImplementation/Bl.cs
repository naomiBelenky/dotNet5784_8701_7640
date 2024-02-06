namespace BlImplementation;
using BlApi;
using BO;
using System.Xml.Linq;

internal class Bl :IBl
{
    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();

    public IMilestone MileStone => throw new NotImplementedException();
    public void schedule()
    {
        Console.WriteLine("please enter the date that you want to start the project\n");
        DateTime startProject = Convert.ToDateTime(Console.ReadLine());
        //צריך לשמור את התאריך איכשהו בקובץ אקס אמ אל
        IEnumerable<TaskInList> tasks = Task.ReadAll();
        foreach (var task in tasks)
        {
            Task fullTask = Task.Read(task.Id) ?? throw new BO.BlDoesNotExistException($"Task with ID={task.Id} does not exist");
        }
    }
}
