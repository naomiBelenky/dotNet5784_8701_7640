

using BlApi;
using BO;



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
        
        //}


    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

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
