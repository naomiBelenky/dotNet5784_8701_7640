using BlApi;
using System.Collections.Generic;
using System.Linq;

namespace BlImplementation;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public void Add(BO.Engineer item)
    {
        DO.Engineer doEng = new DO.Engineer(item.Id, item.Name, item.Email, (DO.EngineerLevel)item.Level, item.Cost);


        try
        {
            if (item.Id > 0 && item.Name != "" && item.Cost > 0 && item.Email.EndsWith("@gmail.com"))
            {
                _dal.Engineer.Create(doEng);
            }
            else throw new BO.BlDoesNotExistException("id or name or cost or email are not valid");
        }
        catch (DO.DalAlreadyExistsException messege)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={item.Id} already exists", messege);
        }
    }

    public void Delete(int id)
    {

        var task = _dal.Task.Read(item => item.EngineerID == id &&
           item.StartWork < DateTime.Now);
        if (task != null)
        {
            throw new BO.BlDeletionImpossible("Deletion is impossible because the engineer is/was working on a task");
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
        DO.Engineer? doEng = _dal.Engineer.Read(id); //קוראים את הנתונים
        if (doEng == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");

        BO.Engineer eng = new BO.Engineer() //יוצרים ישות לוגית בעזרתם
        {
            Id = id,
            Name = doEng.FullName,
            Email = doEng.Email,
            Level = (BO.EngineerLevel)doEng.Level,
            Cost = doEng.CostPerHour
        };

        var task = _dal.Task.Read(item => item.EngineerID == id &&
           item.StartWork < DateTime.Now && DateTime.Now < item.Deadline); //משתנה עזר שבודק האם יש משימה 

        if (task!=null) //אם יש אז נעדכן אותה 
        {
            BO.TaskInEngineer temp = new BO.TaskInEngineer() { Id= task.TaskID, Name= task.Name };
            eng.Task = temp;
        }

        return eng;
    }

    public IEnumerable<BO.Engineer> ReadAll(Func<bool> filter)
    {
        if (filter == null)
        {
            //return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
            //        select new BO.Engineer()
            //        {
            //            Id = doEngineer.EngineerID,
            //            Name = doEngineer.FullName,
            //            Email = doEngineer.Email,
            //            Level = (BO.EngineerLevel)doEngineer.Level,
            //            Cost = doEngineer.CostPerHour,
            //        });
            return _dal.Engineer.ReadAll().Select(doEngineer => new BO.Engineer
            {
                Id = doEngineer.EngineerID,
                Name = doEngineer.FullName,
                Email = doEngineer.Email,
                Level = (BO.EngineerLevel)doEngineer.Level,
                Cost = doEngineer.CostPerHour,
            });
        }
        else
        {
            return _dal.Engineer.ReadAll()
                    .Where(doEngineer => filter())
                    .Select(doEngineer => new BO.Engineer
                    {
                        Id = doEngineer.EngineerID,
                        Name = doEngineer.FullName,
                        Email = doEngineer.Email,
                        Level = (BO.EngineerLevel)doEngineer.Level,
                        Cost = doEngineer.CostPerHour
                    });
        }
    }

    public void Update(BO.Engineer engineer)
    {
        DO.Engineer doEng = new DO.Engineer(engineer.Id, engineer.Name, engineer.Email, (DO.EngineerLevel)engineer.Level, engineer.Cost);
        try
        {
            if (engineer.Id > 0 && engineer.Name != "" && engineer.Email.EndsWith("@gmail.com") && engineer.Cost > 0)
                _dal.Engineer.Update(doEng);
            else throw new BO.BlDoesNotExistException("id or name or cost or email are not valid");
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={engineer.Id} does Not exist");
        }
    }
}
