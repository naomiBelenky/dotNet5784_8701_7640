namespace Dal;
using DalApi;
using DO;

public class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        if (DataSource.Engineers.Find(engineer => engineer.EngineerID == item.EngineerID) != null)
            throw new Exception("Engineer with this id already exists");
        DataSource.Engineers.Add(item);
        return item.EngineerID;
    }

    public void Delete(int id)
    {
        //if (DataSource.Tasks.ForEach(task => task.EngineerId==id))
        Engineer? temp=DataSource.Engineers.Find(eng => eng.EngineerID == id);
        if (temp==null)
            throw new Exception($"Engineer with ID={id} does Not exist");
        DataSource.Engineers.Remove(temp);
    }

    public Engineer? Read(int id)
    {
        return (DataSource.Engineers.Find(engineer => engineer.EngineerID == id));
    }

    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
    }

    public void Update(Engineer item)
    {
        Engineer? temp = DataSource.Engineers.Find(engineer => engineer.EngineerID == item.EngineerID);
        if (temp == null)
            throw new Exception($"Engineer with ID={item.EngineerID} does Not exist");
        DataSource.Engineers.Remove(temp);
        DataSource.Engineers.Add(item);
    }
}
