﻿namespace BlApi;

public interface ITask
{
    /// <summary>
    /// Returns all tasks According to the condition of the function. If no function is null returns them all
    /// </summary>
    /// <param name="filter"> optional filtering of the tasks to read </param>
    /// <returns></returns>
    public IEnumerable<BO.TaskInList> ReadAll(Func<BO.Task, bool>? filter = null); //Funk needs to get paramater?
    /// <summary>
    /// gets id and returns its task (throws exeption if doesnt find)
    /// </summary>
    /// <param name="id"> Id ot the task to read </param>
    /// <returns></returns>
    public BO.Task Read(int id);
    /// <summary>
    /// gets a task and adds it to the data.
    /// </summary>
    /// <param name="task"> BO Task to be added </param>
    public int Add(BO.Task task);
    /// <summary>
    /// gets a task and update the current task (throws exeption if doesnt find)
    /// </summary>
    /// <param name="task"> BO Task to update </param>
    public void Update(BO.Task task);
    /// <summary>
    /// gets task id and deletes it from the data. (throws exeption if doesnt find)
    /// </summary>
    /// <param name="id"> Id of the task to delete </param>
    public void Delete(int id);
    /// <summary>
    /// gets task id and date, and updates the date in the matching task. (throws exeption if doesnt find)
    /// </summary>
    /// <param name="id"> Id of the task to update </param>
    /// <param name="date"> new Date to update </param>
    public void UpdateDate(int id, DateTime date);

    /// <summary>
    /// check if the second task can be depended on the first task withot curcle dependencies
    /// </summary>
    /// <param name="idPrevTask"></param>
    /// <param name="idNextTask"></param>
    /// <returns></returns>
    public bool checkLink(int idPrevTask, int idNextTask);

    public DateTime? SuggestStartDate(int id);

    /// <summary>
    /// get task and check if its depended on tasks that sisnt finish yet
    /// </summary>
    /// <param name="task"></param>
    /// <returns></returns>

    public bool NodidntFinishLink(BO.Task task);
}
