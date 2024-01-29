

using BlApi;
using DO;

namespace BlImplementation;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="task"></param>
    /// <exception cref="BlInformationIsntValid"></exception>
    /// <exception cref="BO.BlAlreadyExistsException"></exception>
    public void Add(BO.Task task)
    {
        DO.Task doTask = new DO.Task(task.Id, task.Name, task.Description);
        try
        {
            if (int.IsNegative(task.Id)) throw new BlInformationIsntValid("id is not valid");
            if (task.Name == "") throw new BlInformationIsntValid("name is not valid");
            int id = _dal.Task.Create(doTask);
            
        }
        catch (DO.DalAlreadyExistsException ex) {
            throw new BO.BlAlreadyExistsException($"Task with ID={task.Id} already exists\", messege");
        }
        
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public System.Threading.Tasks.Task? Read(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BO.Task> ReadAll(Func<bool> filter)
    {
        throw new NotImplementedException();
    }

    public void Update(BO.Task task)
    {
        throw new NotImplementedException();
    }

    public void UpdateDate(int id, DateTime date)
    {
        throw new NotImplementedException();
    }
}
