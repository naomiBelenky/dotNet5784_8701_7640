namespace Dal;

using DalApi;
using DO;
using System.Collections.Generic;

internal class LinkImplementation : ILink
{
    public int Create(Link item)
    {
        int newID = DataSource.Config.NextLinkID;
        Link newLink = item with { LinkID = newID };
        DataSource.Links.Add(newLink);
        return newID;
    }

    public void Delete(int id)
    {
        Link? temp = DataSource.Links.Find(link => link.LinkID == id);
        if (temp == null)
            throw new Exception($"Link with ID={id} does Not exist");
        DataSource.Links.Remove(temp);
    }

    public Link? Read(int id)
    {
        foreach (Link item in DataSource.Links)
            if (item.LinkID == id)
                return item;

        return null;

        //return (DataSource.Links.Find(link => link.LinkID == id)); //Stage 2
    }

    public IEnumerable<Link> ReadAll(Func<Link, bool>? filter = null)
    {
        if (filter != null)
        {
            return from item in DataSource.Links
                   where filter(item)
                   select item;
        }

        return from item in DataSource.Links
               select item;
        //return new List<Link>(DataSource.Links); //Stage 2
    }

    public void Update(Link item)
    {
        Link? temp = DataSource.Links.Find(link => link.LinkID == item.LinkID);
        if (temp == null)
            throw new Exception($"Link with ID={item.LinkID} does Not exist");
        DataSource.Links.Remove(temp);
        DataSource.Links.Add(item);
    }
}
