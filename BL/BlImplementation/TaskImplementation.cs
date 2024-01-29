using BlApi;

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
            if (int.IsNegative(task.Id)) throw new BO.BlInformationIsntValid("id is not valid");
            if (task.Name == "") throw new BO.BlInformationIsntValid("name is not valid");
            if (task.Links != null) //if there are dependent tasks, adding them to the list of link
            {
                foreach (BO.TaskInList item in task.Links)
                {
                    DO.Link link = new DO.Link(0, item.Id, task.Id);
                    _dal.Link.Create(link);
                }
            }

            int id = _dal.Task.Create(doTask);
        }
        catch (DO.DalAlreadyExistsException message)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={task.Id} already exists", message);
        }
        
    }

   
    public void Delete(int id)
    {

        //checking if there is another task that depended in this task
        DO.Link? tempLink = _dal.Link.Read(item => item.PrevTask == id);
        if (tempLink != null)
            throw new BO.BlDeletingIsForbidden("Deleting this task is forbiden");

        try
        {
            //check if 
            DO.Task? tempTask = _dal.Task.Read(id);
            if (tempTask.PlanToStart != null)
                throw new BO.BlDeletingIsForbidden("Deleting is forbiden now");

                _dal.Task.Delete(id);
        }
        catch (DO.DalDoesNotExistException messege)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist", messege);
        }  
    }

    public System.Threading.Tasks.Task? Read(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BO.Task> ReadAll(Func<bool>? filter = null)
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
