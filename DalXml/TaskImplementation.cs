
namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;



internal class TaskImplementation: ITask
{
    readonly string s_tasks_xml = "tasks";

    public int Create(Task item)
    {
        List<Task> tasks = new List<Task>();
        tasks = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);
        int nextID = Config.NextTaskId;
        Task copy=item with { TaskID = nextID };
        tasks.Add(copy);
        XMLTools.SaveListToXMLSerializer<Task>(tasks, s_tasks_xml);
        return nextID;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task? Read(Func<Task, bool> filter)
    {
        throw new NotImplementedException();
    }

    public Task? Read(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(Task item)
    {
        throw new NotImplementedException();
    }
}
