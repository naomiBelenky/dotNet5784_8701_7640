using DalApi;
namespace Dal;
using DO;
using System.Collections.Generic;

internal class TaskImplementation : ITask
{
    public int Create(Task item) //Creates new entity object in DAL
    {
        int newID = DataSource.Config.NextTaskID;
        Task newTask = item with { TaskID = newID };
        DataSource.Tasks.Add(newTask);
        return newID;
    }

    public void Delete(int id) //Deletes an object by its Id
    {
        //Task? temp = DataSource.Tasks.Find(task => task.TaskID == id);
        //if (temp == null)

        if (Read(id) == null) //If this id doesnt exist
            throw new DalDoesNotExistException($"Task with ID={id} does Not exist");

        DataSource.Tasks.Remove(temp);
    }

    public Task? Read(int id) //Reads entity object by its ID
    {
        foreach (Task item in DataSource.Tasks)
            if (item.TaskID == id)
                return item;

        return null;
        //return (DataSource.Tasks.Find(task => task.TaskID == id)); //Stage 2
    }



    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter) //Reads all entity objects
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


    public Task? Read(Func<Task, bool> filter) //Reads entity object
    {
        foreach (Task item in DataSource.Tasks)
            if (filter(item))
                return item;

        return null;
    }


    public void Update(Task item) //Updates entity object
    {
        //Task? temp = DataSource.Tasks.Find(task => task.TaskID == item.TaskID); //stage 1
        //if (temp == null)

        if (Read(item.TaskID) == null) //If this id doesnt exist
            throw new DalDoesNotExistException($"Task with ID={item.TaskID} does Not exist");
        DataSource.Tasks.Remove(temp);
        DataSource.Tasks.Add(item);
    }
}