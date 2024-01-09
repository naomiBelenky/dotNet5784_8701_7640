namespace Dal;

using DO;
using DalApi;
using DO;

internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        if (DataSource.Engineers.Find(eng => eng.EngineerID == item.EngineerID) != null)
            throw new Exception("Engineer with this ID already exist!!");

        DataSource.Engineers.Add(item);
        return item.EngineerID;
    }

    public void Delete(int id)
    {
        Engineer? temp = DataSource.Engineers.Find(eng => eng.EngineerID == id);
        if (temp == null) 
            throw new Exception($"Engineer with ID={id} does Not exist");
        DataSource.Engineers.Remove(temp);
    }

    public Engineer? Read(int id)
    {
        return (DataSource.Engineers.Find(eng => eng.EngineerID == id));
    }

    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
    }

    public void Update(Engineer item)
    {
        Engineer? temp = DataSource.Engineers.Find(eng => eng.EngineerID == item.EngineerID);
        if (temp == null)
            throw new Exception($"Engineer with ID={item.EngineerID} does Not exist");
        DataSource.Engineers.Remove(temp);
        DataSource.Engineers.Add(item);
    }
}
