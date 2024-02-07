namespace BlImplementation;
using BlApi;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public int Add(BO.Task task)
    {

        if (Factory.Get().getStage() != (BO.Stage.Planning)) throw new BO.BlForbiddenInThisStage("Can not add tasks after scheduling the project");

        DO.Task doTask = new DO.Task(task.Id, task.Name, task.Description, (DO.Level)task.Difficulty) with { TimeForTask = task.Duration };
        try
        {
            if (task.Id < 0) throw new BO.BlInformationIsntValid("id is not valid");
            if (task.Name == "") throw new BO.BlInformationIsntValid("name is not valid");
            

            int id = _dal.Task.Create(doTask);  //if the data is valid, creating the task in the data layer
            if (task.Links != null) //if there are dependent tasks, adding them to the list of links
            {
                foreach (BO.TaskInList item in task.Links)
                {
                    DO.Link link = new DO.Link(0, item.Id, id);
                    _dal.Link.Create(link);
                }
            }
            return id;
        }
        catch (DO.DalAlreadyExistsException message)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={task.Id} already exists", message);
        }

    }


    public void Delete(int id)
    {
        if (Factory.Get().getStage() != (BO.Stage.Planning)) throw new BO.BlForbiddenInThisStage("Can not delete tasks after scheduling the project");
        //checking if there is another task that depended in this task
        DO.Link? tempLink = _dal.Link.Read(item => item.PrevTask == id);
        if (tempLink != null)
            throw new BO.BlForbiddenInThisStage("Deleting this task is forbidden beacuse there is another task that depended on it");

        try
        {
            DO.Task? tempTask = _dal.Task.Read(id);
            if (tempTask==null) throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
            else if (tempTask.PlanToStart != null) throw new BO.BlForbiddenInThisStage("Deleting is prohibited after the project schedule is created");

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
            //Links calculateed later
            PlanToStart = doTask.PlanToStart,
            StartWork = doTask.StartWork,
            //PlanToFinish calculated later
            Deadline = doTask.Deadline,
            FinishDate = doTask.FinishDate,
            Duration = doTask.TimeForTask,
            Product = doTask.Product,
            Notes = doTask.Notes,

        };
        if (doTask.EngineerID != null)
        {
            task.Engineer = new BO.EngineerInTask    //filling the info about the engineer working on the task
            {
                Id = (int)doTask.EngineerID!,
                Name = (_dal.Engineer.Read(id) ??
                    throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist")).FullName
                //if the Read returns an engineer, assigning his name to the EngineerInTask
            };
        }
        task.Status = getStatus(doTask);
        task.PlanToFinish = getPlanToFinish(doTask);
        task.Links = getLinks(task);

        return task;
    }

    public IEnumerable<BO.TaskInList> ReadAll(Func<bool>? filter = null)
    {
        if (filter == null)
        {
            //IEnumerable<BO.TaskInList> tasks1=(_dal.Task.ReadAll()).Select new BO.TaskInList()
            IEnumerable<BO.TaskInList> tasks = (from DO.Task item in _dal.Task.ReadAll()
                                                select new BO.TaskInList()
                                                {
                                                    Id=item.TaskID!,
                                                    Name = item.Name,
                                                    Description = item.Description,
                                                    Status=getStatus(item)
                                                });
            return tasks;
        }
        else
        {
            // IEnumerable<BO.TaskInList> tasks1=(_dal.Task.ReadAll()).Where(filter()).select new 
            IEnumerable<BO.TaskInList> tasks = (from DO.Task item in _dal.Task.ReadAll()
                                                where filter()
                                                select new BO.TaskInList()
                                                {
                                                    Id = item.TaskID!,
                                                    Name = item.Name,
                                                    Description = item.Description,
                                                    Status = getStatus(item)
                                                });
            return tasks;
        }

    }

    public void Update(BO.Task task)
    {
        //check if data is valid
        if (task.Id <= 0) throw new BO.BlInformationIsntValid("id is not valid");
        if (task.Name == "") throw new BO.BlInformationIsntValid("name is not valid");

        try
        {
            //checking how many links there are in the data
            int counterDoLinks = (_dal.Link.ReadAll(link => link.NextTask == task.Id).ToList().Count);

            //Check whether the schedule has already been set
            DO.Task? tempTask = _dal.Task.Read(task.Id);
            if (tempTask != null)
                if (tempTask.PlanToStart != null)
                {
                    //If the schedule has already been set, check that only the fields allowed for update have been updated
                    if (task.Id != tempTask.TaskID || (int)task.Difficulty != (int)tempTask.Difficulty ||/*task.Milestone!=tempTask.Milestone||*/
                        task.Creation != tempTask.Creation || task.PlanToStart != tempTask.PlanToStart
                        || task.StartWork != tempTask.StartWork || task.Deadline != tempTask.Deadline
                        || task.FinishDate != tempTask.FinishDate || task.Links != null && task.Links.Count != counterDoLinks)
                    { throw new BO.BlForbiddenInThisStage("Updating this parameters is prohibited after the project schedule is created"); }
                }

            //If we are here, it means that all the tests passed successfully:)

            //Check whether links need to be added or be deleted
            if (task.Links != null)
            {
                if (task.Links.Count > counterDoLinks) //if links need to be added
                {
                    IEnumerable<int> newTasksID = (from BO.TaskInList item in task.Links
                                                   let doLink = _dal.Link.Read(link => link.PrevTask == item.Id && link.NextTask == task.Id)
                                                   //group item by doLink == null ? "newTask" : "dontNew");
                                                   where (doLink == null)
                                                   select item.Id); //list of new links 

                    foreach (var taskID in newTasksID) //add all the new links
                    {
                        DO.Link newLink = new DO.Link(0, taskID, task.Id);
                        _dal.Link.Create(newLink);
                    }
                }

                if (task.Links.Count < counterDoLinks)  //if links need to be deleted
                {
                    //Search for all links in data that no longer exists in the list
                    IEnumerable<int> oldLinksID = (from DO.Link item in _dal.Link.ReadAll(link => link.NextTask == task.Id)
                                                   where task.Links.Any(temp => temp.Id == item.PrevTask) == false
                                                   select item.LinkID);
                    foreach (int linkID in oldLinksID)
                    {
                        _dal.Link.Delete(linkID);
                    }
                }
            }
            else  //if the list is empty
            {
                if (counterDoLinks > 0) //if links need to be deleted
                {
                    IEnumerable<DO.Link> links = _dal.Link.ReadAll(item => item.NextTask == task.Id)!;
                    foreach (DO.Link link in links) { _dal.Link.Delete(link.LinkID); }
                }
            }

            int? tempID;
            if (task.Engineer == null)
                tempID = null;
            else
                tempID = task.Engineer.Id;

            DO.Task doTask = new DO.Task(task.Id, task.Name, task.Description, (DO.Level)task.Difficulty,
                false, task.Creation, task.PlanToStart, task.StartWork, task.Duration, task.Deadline,
                task.FinishDate, task.Product, task.Notes, tempID);
            _dal.Task.Update(doTask);
        }

        catch (DO.DalDoesNotExistException messege)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={task.Id} does Not exist", messege);
        }
    }

    public void UpdateDate(int id, DateTime date)
    {
        DO.Task doTask = _dal.Task.Read(id) ?? throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");   //reading the DO task
        BO.Task task = Read(id)!;
        List<BO.TaskInList>? links = task.Links;
        if (links == null)
        {
            _dal.Task.Update(doTask with { PlanToStart = date });
            return;   //if there are no tasks that this task depends on, simply updating the PlanToStart date
        }

        //if there are tasks that this task depends on:
        foreach (BO.TaskInList dependOnTask in links)
        {
            DO.Task fullTask = _dal.Task.Read(dependOnTask.Id) ?? throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
            if (fullTask.PlanToStart == null) throw new BO.BlForbiddenInThisStage($"Task with ID={dependOnTask} hasn't been schedualed yet");
            if (date < getPlanToFinish(fullTask)) throw new BO.BlForbiddenInThisStage($"Task with ID={dependOnTask.Id} isn't planned to be finished in time");
        }

        //if the tasks are schedualed and planned to be finished before the date:
        _dal.Task.Update(doTask with { PlanToStart = date });


    }

    #region private methods for help

    /// <summary>
    /// determines the status of the task
    /// </summary>
    /// <param name="task"> The task we are trying to get the status of </param>
    /// <returns></returns>
    private BO.Status getStatus(DO.Task task)
    {
        BO.Status status = BO.Status.Unscheduled;
        if (task.PlanToStart == null) status = BO.Status.Unscheduled;
        else if (task.StartWork == null) status = BO.Status.Scheduled;
        else if (task.FinishDate == null) status = BO.Status.OnTrack;
        else if (task.FinishDate <= DateTime.Now) status = BO.Status.Done;
        return status;
    }
    /// <summary>
    /// calculates the date that is planned to finish the task
    /// </summary>
    /// <param name="task"> the task we are calculating the date for </param>
    /// <returns></returns>
    private DateTime? getPlanToFinish(DO.Task task)
    {
        if (task.PlanToStart == null) return null;
        if (task.TimeForTask == null) return null;
        return (task.PlanToStart + task.TimeForTask);
    }
    /// <summary>
    /// returns the list of tasks which thus task depends on
    /// </summary>
    /// <param name="task"> the task that depends on the list of links </param>
    /// <returns></returns>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    private List<BO.TaskInList> getLinks(BO.Task task)
    {
        List<DO.Link> links = new List<DO.Link>(_dal.Link.ReadAll(link => link.NextTask == task.Id));
        if (links.Count == 0) return new List<BO.TaskInList>(); //if the ReadAll didn't return any link, returning an empty list

        List<BO.TaskInList> tasks = new List<BO.TaskInList>();
        foreach (DO.Link link in links)
        {
            DO.Task doTask = _dal.Task.Read(link!.PrevTask) ?? throw new BO.BlDoesNotExistException($"Task with Id={link.PrevTask} does not exist");
            BO.TaskInList newTask = new BO.TaskInList
            {
                Id = doTask.TaskID,
                Name = doTask.Name,
                Description = doTask.Description,
                Status = getStatus(doTask),
            };
            tasks.Add(newTask); //adding each task to the list of TaskInList
        }
        return tasks;
    }
    #endregion

    public DateTime? SuggestStartDate(int id)
    {
        if (Factory.Get().getStage() == BO.Stage.Planning) throw new BO.BlDoesNotExistException("Start date of the project is not set yet");
        DO.Task task = _dal.Task.Read(id) ?? throw new BO.BlDoesNotExistException($"Task with ID={id} does not exist");
        IEnumerable<DO.Link> links = _dal.Link.ReadAll(link => link.NextTask == id);    //getting all the tasks that our task depends on
        if (links == null) return _dal.getStartOrFinshDatesFromXml("startDate");    //if the task does not depend on any task, it can start when the project starts

        IEnumerable<DateTime?> dates = links.Select
            (link => getPlanToFinish(_dal.Task.Read(link.PrevTask)  //making a collection of the PlanToFinish dates of the tasks
            ?? throw new BO.BlDoesNotExistException($"Task with ID={id} does not exist")));  //if one of the tasks doesn't have a startDate yet

        if (dates.Any(date => date == null)) return null;   //if one of the tasks doesn't have a startDate yet
        DateTime? suggestedDate = dates.Max();

        return suggestedDate;
    }
}
