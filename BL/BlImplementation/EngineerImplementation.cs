﻿using BlApi;

namespace BlImplementation;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    
    public void Add(BO.Engineer item)
    {
        DO.Engineer doEng = new DO.Engineer(item.Id, item.Name, item.Email, (DO.Level)item.Level, item.Cost);

        try
        {
            if (item.Id <= 0) throw new BO.BlInformationIsntValid("id is not valid");
            if (string.IsNullOrEmpty(item.Name)) throw new BO.BlInformationIsntValid("name is not valid");
            if (!item.Email.EndsWith("@gmail.com")) throw new BO.BlInformationIsntValid("email adress is not valid");
            if (double.IsNegative(item.Cost)) throw new BO.BlInformationIsntValid("cost is not valid");

            _dal.Engineer.Create(doEng);          
        }
        catch (DO.DalAlreadyExistsException messege)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={item.Id} already exists", messege);
        }
    }

    public void Delete(int id)
    {

        //check if there is a task that the engineer already started working on
        var task = _dal.Task.Read(item => item.EngineerID == id &&
           item.StartWork < DateTime.Now);
        if (task != null) //if found 
        {
            throw new BO.BlForbiddenInThisStage("Deletion is impossible because the engineer is/was working on a task");
        }

        try
        {
            _dal.Engineer.Delete(id);
        }
        catch (DO.DalDoesNotExistException messege)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist", messege);
        }

    }

    public BO.Engineer? Read(int id)
    {
        DO.Engineer? doEng = _dal.Engineer.Read(id); 
        if (doEng == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");

        BO.Engineer eng = new BO.Engineer()
        {
            Id = id,
            Name = doEng.FullName,
            Email = doEng.Email,
            Level = (BO.Level)doEng.Level,
            Cost = doEng.CostPerHour
        };

        //check if there is a task on track of the engineer
        var task = _dal.Task.Read(item => item.EngineerID == id /*&&
           item.StartWork < DateTime.Now && DateTime.Now < item.Deadline*/); 

        if (task!=null) //if found 
        {
            BO.TaskInEngineer temp = new BO.TaskInEngineer() { Id= task.TaskID, Name= task.Name };
            eng.Task = temp;
        }

        return eng;
    }

    public IEnumerable<BO.Engineer> ReadAll(Func<bool>? filter = null)
    {
        if (filter == null)
        {
            IEnumerable<BO.Engineer> temp = (from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
                                             select new BO.Engineer()
                                             {
                                                 Id = doEngineer.EngineerID,
                                                 Name = doEngineer.FullName,
                                                 Email = doEngineer.Email,
                                                 Level = (BO.Level)doEngineer.Level,
                                                 Cost = doEngineer.CostPerHour,
                                             });
            foreach (BO.Engineer engineer in temp)
            {
                DO.Task? task = _dal.Task.Read(item => item.EngineerID == engineer.Id);   //searching for the task that the engineer is responsible for
                if (task != null)
                    engineer.Task = new BO.TaskInEngineer { Id = task.TaskID, Name = task.Name };
            }
            return temp;
        }
        else
        {
            IEnumerable<BO.Engineer> temp = (from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
                                             where filter()
                                             select new BO.Engineer()
                                             {
                                                 Id = doEngineer.EngineerID,
                                                 Name = doEngineer.FullName,
                                                 Email = doEngineer.Email,
                                                 Level = (BO.Level)doEngineer.Level,
                                                 Cost = doEngineer.CostPerHour,
                                             });
            foreach (BO.Engineer engineer in temp)
            {
                DO.Task? task = _dal.Task.Read(item => item.EngineerID == engineer.Id);   //searching for the task that the engineer is responsible for
                if (task != null)
                    engineer.Task = new BO.TaskInEngineer { Id = task.TaskID, Name = task.Name };
            }
            return temp;
        }
    }

    public void Update(BO.Engineer engineer)
    {
        DO.Engineer doEng = new DO.Engineer(engineer.Id, engineer.Name, engineer.Email, (DO.Level)engineer.Level, engineer.Cost);
        try
        {
            if (int.IsNegative(engineer.Id)) throw new BO.BlInformationIsntValid("id is not valid");
            if (string.IsNullOrEmpty(engineer.Name)) throw new BO.BlInformationIsntValid("name is not valid");
            if (!engineer.Email.EndsWith("@gmail.com")) throw new BO.BlInformationIsntValid("email adress is not valid");
            if (double.IsNegative(engineer.Cost)) throw new BO.BlInformationIsntValid("cost is not valid");

            _dal.Engineer.Update(doEng);    //if the information is valid, update the engineer in the data layer

            if (engineer.Task is not null)
            {
                var task = _dal.Task.Read(task => task.TaskID == engineer.Task!.Id) //reading the task that the engineer is responsible for
                    ?? throw new BO.BlDoesNotExistException($"task with id={engineer.Task.Id} does not exist");

                _dal.Task.Update(task with { EngineerID = engineer.Id });   //if there is a task, update the task that this engineer is working on it
            }

        }
        catch (DO.DalDoesNotExistException message)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={engineer.Id} does Not exist", message);
        }
    }
}
