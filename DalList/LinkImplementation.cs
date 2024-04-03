namespace Dal;

using DalApi;
using DO;
using System.Collections.Generic;

internal class LinkImplementation : ILink
{
    public int Create(Link item) //Creates new entity object in DAL
    {
        int newID = DataSource.Config.NextLinkID;
        Link newLink = item with { LinkID = newID };
        DataSource.Links.Add(newLink);
        return newID;
    }

    public void Delete(int id) //Deletes an object by its Id
    {
        //Link? temp = DataSource.Links.Find(link => link.LinkID == id); //Stage 1
        //if (temp == null)

        Link? temp = Read(id);
        if (temp == null) //If this id doesnt exist
            throw new DalDoesNotExistException($"Link with ID={id} does Not exist");
        DataSource.Links.Remove(temp);
    }

    public Link? Read(int id) //Reads entity object by its ID
    {
        foreach (Link item in DataSource.Links)
            if (item.LinkID == id)
                return item;

        return null;    //didn't find

        //return (DataSource.Links.Find(link => link.LinkID == id)); //Stage 2
    }

    public IEnumerable<Link> ReadAll(Func<Link, bool>? filter) //Reads all entity objects
    {
        if (filter != null)
        {
            return from item in DataSource.Links
                   where filter(item)
                   select item;
        }

        return from item in DataSource.Links    //if there is no filter, returning the whole list
               select item;

        //return new List<Link>(DataSource.Links); //Stage 2
    }

    public void Update(Link item) //Updates entity object
    {
        //Link? temp = DataSource.Links.Find(link => link.LinkID == item.LinkID); //stage 1
        //if (temp == null)

        Link? temp = Read(item.LinkID);
        if (temp == null) //If this id doesnt exist
            throw new DalDoesNotExistException($"Link with ID={item.LinkID} does Not exist");
        DataSource.Links.Remove(temp);  //deleting the existing object
        DataSource.Links.Add(item); //adding the updated object
    }

    public Link? Read(Func<Link, bool> filter) //Reads entity object
    {
        foreach (Link item in DataSource.Links)
            if (filter(item))
                return item;

        return null;    //didn't find
    }

    public void DeleteAll() //for stage 3
    {
        DataSource.Links.Clear();
        DataSource.Config.ResetLinkID();
    }

}
