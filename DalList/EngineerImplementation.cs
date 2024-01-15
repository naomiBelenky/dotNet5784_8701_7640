﻿namespace Dal;

using DO;
using DalApi;
using DO;

internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        //if (DataSource.Engineers.Find(eng => eng.EngineerID == item.EngineerID) != null)
        if(Read(item.EngineerID) is not null)
            throw new DalAlreadyExistsException($"Engineer with ID={item.EngineerID} already exist!!");

        DataSource.Engineers.Add(item);
        return item.EngineerID;
    }

    public void Delete(int id)
    {
        Engineer? temp = DataSource.Engineers.Find(eng => eng.EngineerID == id);
        if (temp == null) 
            throw new DalDoesNotExistException($"Engineer with ID={id} does Not exist");
        DataSource.Engineers.Remove(temp);
    }

    public Engineer? Read(int id)
    {

        foreach (Engineer item in DataSource.Engineers)
            if (item.EngineerID == id)
                return item;
        
        return null; //didnt found
        //return (DataSource.Engineers.Find(eng => eng.EngineerID == id)); //Stage 1
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter)
    {
        if (filter != null)
        {
            return from item in DataSource.Engineers
                   where filter(item)
                   select item;
        }

        return from item in DataSource.Engineers
               select item;

        //return new List<Engineer>(DataSource.Engineers); //Stage 1
    }

    public void Update(Engineer item)
    {
        Engineer? temp = DataSource.Engineers.Find(eng => eng.EngineerID == item.EngineerID);
        if (temp == null)
            throw new DalDoesNotExistException($"Engineer with ID={item.EngineerID} does Not exist");
        DataSource.Engineers.Remove(temp);
        DataSource.Engineers.Add(item);
    }

    public Engineer? Read(Func<Engineer, bool> filter) //Reads entity object by its ID
    {
        foreach (Engineer item in DataSource.Engineers)
            if (filter(item))
                return item;

        return null;
    }
}
