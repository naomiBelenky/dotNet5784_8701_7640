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
        return (DataSource.Links.Find(link => link.LinkID == id));
    }

    public List<Link> ReadAll()
    {
        return new List<Link>(DataSource.Links);
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
