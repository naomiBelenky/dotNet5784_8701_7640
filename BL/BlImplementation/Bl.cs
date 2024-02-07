namespace BlImplementation;
using BlApi;
using BO;
using Dal;
using System.Xml.Linq;

internal class Bl : IBl
{
    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();

    public IMilestone MileStone => throw new NotImplementedException();

    public BO.Stage StageOfProject { get => StageOfProject; set => StageOfProject = value; }

    public Stage getStage()
    {
        XElement root = XMLTools.LoadListFromXMLElement("data_config.xml");
        DateTime? date = root.ToDateTimeNullable("startDate");
        if (date == null)
            return Stage.Planning;
        else return Stage.Execution;
    }

    public void automaticSchedule()
    {
        //StageOfProject = BO.Stage.Execution;

        List<BO.TaskInList> tasks = (Task.ReadAll()).ToList();
        foreach (var task in tasks)
        {

            DateTime date = Task.SuggestStartDate(task.Id);

            if (date < Task.SuggestStartDate(task.Id))
            {
                Console.WriteLine("not valid date, there are later task that this task depend on them");
                //throw new BO.BlForbiddenInThisStage("not valid date, there are later task that this task depend on them");
                tasks.Remove(task); 
                tasks.Add(task); //enter the task to the end of the list
                

            }

            // Task.UpdateDate(task.Id, Task.SuggestStartDate(task.Id));

            Task.UpdateDate(task.Id, Task.SuggestStartDate(task.Id));
        }


    }
}
