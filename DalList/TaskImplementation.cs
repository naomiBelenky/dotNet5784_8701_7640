using DalApi;
namespace Dal;
using DO;

public class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        int newId = DataSource.Config.NextTaskID;
        Task newTask = item with { TaskID = newId };
        DataSource.Tasks.Add(newTask);
        return newId;
    }

    public void Delete(int id)
    {
        Task? temp = DataSource.Tasks.Find(task=>task.TaskID == id);
        if (temp == null)
            throw new Exception($"Task with ID={id} does Not exist");
        DataSource.Tasks.Remove(temp);
    }

    public Task? Read(int id)
    {
        return (DataSource.Tasks.Find(task => task.TaskID == id));
    }

    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
    }

    public void Update(Task item)
    {
        Task? temp = DataSource.Tasks.Find(task => task.TaskID == item.TaskID);
        if (temp == null)
            throw new Exception($"Task with ID={item.TaskID} does Not exist");
        DataSource.Tasks.Remove(temp);
        DataSource.Tasks.Add(item);
    }
}
