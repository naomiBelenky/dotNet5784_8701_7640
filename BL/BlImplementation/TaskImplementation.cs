using BlApi;

namespace BlImplementation;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public void Add(BO.Task task)
    {
        DO.Task doTask = new DO.Task(task.Id, task.Name, task.Description, (DO.Level)task.Difficulty);
        try
        {
            if (int.IsNegative(task.Id)) throw new BO.BlInformationIsntValid("id is not valid");
            if (task.Name == "") throw new BO.BlInformationIsntValid("name is not valid");
            if (task.Links != null) //if there are dependent tasks, adding them to the list of links
            {
                foreach (BO.TaskInList item in task.Links)
                {
                    DO.Link link = new DO.Link(0, item.Id, task.Id);
                    _dal.Link.Create(link);
                }
            }

            int id = _dal.Task.Create(doTask);  //if the data is valid, creating the task in the data layer
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
            if (tempTask != null)
                if (tempTask.PlanToStart != null)
                    throw new BO.BlDeletingIsForbidden("Deleting is forbiden now");
                else;
            else
                throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
            

            _dal.Task.Delete(id);
        }
        catch (DO.DalDoesNotExistException messege)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist", messege);
        }
    }

   
    public BO.Task? Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id) ?? throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        BO.Task task = new BO.Task()
        {
            Id = id,
            Name = doTask.Name,
            Description = doTask.Description,
            Difficulty = (BO.Level)doTask.Difficulty,
            Creation = (DateTime)doTask.Creation!,
            //Status calculated later
            //Links (need to calculate)
            //Milestone (idk)
            PlanToStart = doTask.PlanToStart,
            StartWork = doTask.StartWork,
            //PlanToFinish (need to calculate)
            Deadline = doTask.Deadline,
            FinishDate = doTask.FinishDate,
            //Duration = doTask.TimeForTask, (need to fix TimeToStart to be TimeSpan and not double)
            Product = doTask.Product,
            Notes = doTask.Notes,
            Engineer = new BO.EngineerInTask    //filling the info about the engineer working on the task
            {
                Id = (int)doTask.EngineerID!,
                Name = (_dal.Engineer.Read(id)??
                throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist")).FullName
                //if the Read returns an engineer, assigning his name to the EngineerInTask
            }
        };

        task.Status = getStatus(task);

        return task;
    }

        };

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

    #region private methods for help
    /// <summary>
    /// determines the status of the task
    /// </summary>
    /// <param name="task"> The task we are trying to get the status of </param>
    /// <returns></returns>
    private BO.Status getStatus(BO.Task task)
    {
        BO.Status status = BO.Status.Unscheduled;
        if (task.PlanToStart == null) status = BO.Status.Unscheduled;
        else if (task.StartWork == null) status = BO.Status.Scheduled;
        else if (task.FinishDate == null) status = BO.Status.OnTrack;
        else if (task.FinishDate <= DateTime.Now) status = BO.Status.Done;
        return status;
    }

    private DateTime planToFinish(BO.Task task)
    { }

    #endregion
}
