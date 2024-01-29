

namespace BlApi;

public interface ITask
{
    /// <summary>
    /// Returns all tasks According to the condition of the function. If no function is null returns them all
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<BO.Task> ReadAll(Func<bool>? filter = null); //Funk needs to get paramater?
    /// <summary>
    /// gets id and returns its task (throws exeption if doesnt find)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task? Read(int id);
    /// <summary>
    /// gets a task and add it to the data.
    /// </summary>
    /// <param name="task"></param>
    public void Add(BO.Task task);
    /// <summary>
    /// gets a task and update the current task (throws exeption if doesnt find)
    /// </summary>
    /// <param name="task"></param>
    public void Update(BO.Task task);
    /// <summary>
    /// gets task id and deletes it from the data. (throws exeption if doesnt find)
    /// </summary>
    /// <param name="id"></param>
    public void Delete(int id);
    /// <summary>
    /// gets task id and date, and updates the date in the matching task. (throws exeption if doesnt find)
    /// </summary>
    /// <param name="id"></param>
    /// <param name="date"></param>
    public void UpdateDate(int id, DateTime date);
}
