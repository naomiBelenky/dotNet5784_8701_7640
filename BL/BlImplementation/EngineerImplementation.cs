

using BlApi;
using BO;
using System.Collections.Generic;




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
            else throw new BlDoesNotExistException("id or name or cost or email doesnt valid");
        }
        catch (DO.DalAlreadyExistsException messege)
        {
            throw new BlDoesNotExistException($"Engineer with ID={item.Id} already exists", messege);
        }
        //catch (BO.BlDoesNotExistException messege)
        //{ ???????????????
        //I think we dont need it?
        //}
    }

    public void Delete(int id)
    {

    }




    public Engineer? Read(int id)
    {
        DO.Engineer? doEng = _dal.Engineer.Read(id); //קוראים את הנתונים
        if (doEng == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");

        BO.Engineer eng = new Engineer() //יוצרים ישות לוגית בעזרתם
        { Id = id, Name = doEng.FullName, Email = doEng.Email,
            Level = (BO.EngineerLevel)doEng.Level, Cost = doEng.CostPerHour };

        var task = _dal.Task.Read(item => item.EngineerID == id &&
           item.StartWork < DateTime.Now && DateTime.Now < item.Deadline); //משתנה עזר שבודק האם יש משימה 

        if(task!=null) //אם יש אז נעדכן אותה 
        { 
            BO.TaskInEngineer temp=new BO.TaskInEngineer() { Id= task.TaskID, Name= task.Name };
            eng.Task = temp;
        }

        return eng;
    }

    public IEnumerable<Engineer> ReadAll(Func<bool> filter)
{
    throw new NotImplementedException();
}

    public void Update(Engineer engineer)
{
    throw new NotImplementedException();
}
}
