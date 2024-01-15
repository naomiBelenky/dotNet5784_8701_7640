using DalApi;
namespace Dal;
using DO;
using System.Collections.Generic;

internal class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        int newID = DataSource.Config.NextTaskID;
        Task newTask = item with { TaskID = newID };
        DataSource.Tasks.Add(newTask);
        return newID;
    }

    public void Delete(int id)
    {
        Task? temp = DataSource.Tasks.Find(task => task.TaskID == id);
        if (temp == null)
            throw new Exception($"Task with ID={id} does Not exist");
        DataSource.Tasks.Remove(temp);
    }

    public Task? Read(int id)
    {
        foreach (Task item in DataSource.Tasks)
            if (item.TasksID == id)
                return item;

        return null;
        //return (DataSource.Tasks.Find(task => task.TaskID == id)); //Stage 2
    }



    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null) //stage 2
    {
        if (filter != null)
        {
            return from item in DataSource.Tasks
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Tasks
               select item;
    }


    //public IEnumerable<Task> ReadAll(Func<Task>, bool>? filter = null)
    //{

    //    if (filter != null)
    //    {
    //        return from item in DataSource.Tasks
    //               where filter(item)
    //               select item;
    //    }

    //    return from item in DataSource.Tasks
    //           select item;

    //    // return new List<Task>(DataSource.Tasks); //Stage 2
    //}

    public void Update(Task item)
    {
        Task? temp = DataSource.Tasks.Find(task => task.TaskID == item.TaskID);
        if (temp == null)
            throw new Exception($"Task with ID={item.TaskID} does Not exist");
        DataSource.Tasks.Remove(temp);
        DataSource.Tasks.Add(item);
    }
}