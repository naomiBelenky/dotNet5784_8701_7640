

using BlApi;
using DO;
using System.Collections;
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
        catch(DO.DalAlreadyExistsException messege)
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
        DO.Engineer? doEng = _dal.Engineer.Read(id);
        if (doEng == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");

        return new BO.Engineer()
        {
            Id = id,
            Name = doEng.FullName,
            Email = doEng.Email,
            Level = (BO.EngineerLevel)doEng.Level,
            Cost = doEng.CostPerHour,
            //IEnumerable<DO.Task?> temp = from item in _dal.Task.ReadAll()
            //                             where id == item.EngineerID
            //                             select item;
            IEnumerable<DO.Task> temp = _dal.Task.ReadAll(item => item.EngineerID == id);

    
        };

    

    public Engineer? Read(int id)
    {
        throw new NotImplementedException();
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
