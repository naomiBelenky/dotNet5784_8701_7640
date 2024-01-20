namespace Dal;

using DO;
using DalApi;

internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer item) //Creates new entity object in DAL
    {
        //if (DataSource.Engineers.Find(eng => eng.EngineerID == item.EngineerID) != null) //Stage 1

        if (Read(item.EngineerID) is not null) //If this id already exist
            throw new DalAlreadyExistsException($"Engineer with ID={item.EngineerID} already exist!!");

        DataSource.Engineers.Add(item);
        return item.EngineerID;
    }

    public void Delete(int id) //Deletes an object by its Id
    {
        //Engineer? temp = DataSource.Engineers.Find(eng => eng.EngineerID == id); //stage 1

        Engineer? temp = Read(id);
        if (temp == null) //If this id doesnt exist
            throw new DalDoesNotExistException($"Engineer with ID={id} does Not exist");

        DataSource.Engineers.Remove(temp);
    }

    public Engineer? Read(int id) //Reads entity object by its ID
    {

        foreach (Engineer item in DataSource.Engineers)
            if (item.EngineerID == id)
                return item;

        return null; //didn't find
        //return (DataSource.Engineers.Find(eng => eng.EngineerID == id)); //Stage 1
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter) //Reads all entity objects
    {
        if (filter != null)
        {
            return from item in DataSource.Engineers
                   where filter(item)
                   select item;
        }

        return from item in DataSource.Engineers    //if there is no filter, returning the whole list
               select item;

        //return new List<Engineer>(DataSource.Engineers); //Stage 1
    }

    public void Update(Engineer item) //Updates entity object
    {
        //Engineer? temp = DataSource.Engineers.Find(eng => eng.EngineerID == item.EngineerID); //stage 1
        //if (temp == null)

        Engineer? temp = Read(item.EngineerID);
        if (temp == null) //If this id doesn't exist
            throw new DalDoesNotExistException($"Engineer with ID={item.EngineerID} does Not exist");
        DataSource.Engineers.Remove(temp);  //deleting the existing object
        DataSource.Engineers.Add(item); //adding the updated object
    }

    public Engineer? Read(Func<Engineer, bool> filter) //Reads entity object
    {
        foreach (Engineer item in DataSource.Engineers)
            if (filter(item))
                return item;

        return null;    //didn't find
    }

    public void DeleteAll() //for stage 3
    {
        
    }
}
