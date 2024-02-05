
namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;



internal class TaskImplementation : ITask
{
    readonly string s_tasks_xml = "tasks";

    public int Create(Task item)
    {
        List<Task> tasks = new List<Task>();
        tasks = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);
        int nextID = Config.NextTaskId;
        Task copy = item with { TaskID = nextID };
        tasks.Add(copy);
        XMLTools.SaveListToXMLSerializer<Task>(tasks, s_tasks_xml);
        return nextID;
    }

    public void Delete(int id)
    {

        Task? temp = Read(id);
        if (temp == null)
            throw new DalDoesNotExistException($"Task with ID={id} does Not exist");

        List<Task> tasks = new List<Task>();
        tasks = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);
        tasks.Remove(temp); //deleting the item from the list
        XMLTools.SaveListToXMLSerializer(tasks, s_tasks_xml);   //saving the updated list to the xml
    }

    public Task? Read(Func<Task, bool> filter)
    {
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);
        foreach (Task item in tasks)
            if (filter(item))
                return item;

        return null; //didn't find
    }

    public Task? Read(int id)
    {
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);
        foreach (Task item in tasks)
            if (item.TaskID == id) 
                return item;

        return null; //didn't find
    }

    public IEnumerable<Task> ReadAll(Func<Task, bool>? filter = null)
    {
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);
        if (filter != null)
        {
            return from item in tasks
                   where filter(item)
                   select item;
        }

        return from item in tasks    //if there is no filter, returning the whole list
               select item;
    }

    public void Update(Task item)
    {
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);
        Task? temp = Read(item.TaskID);
        if (temp == null)
            throw new DalDoesNotExistException($"Task with ID={item.TaskID} does Not exist");
        tasks.Remove(temp);
        tasks.Add(item);
        XMLTools.SaveListToXMLSerializer(tasks, s_tasks_xml);
    }

    public void DeleteAll() //delete all the antity objects in case of new initialization
    {
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);
        if (tasks.Count != 0)
        {
            tasks.Clear();
            XMLTools.SaveListToXMLSerializer(tasks, s_tasks_xml);
        }
    }
}
