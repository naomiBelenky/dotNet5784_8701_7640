namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Data.Common;

internal class LinkImplementation : ILink
{
    readonly string s_links_xml = "links";

    public int Create(Link item)
    {
        List<Link> links = new List<Link>();
        links = XMLTools.LoadListFromXMLSerializer<Link>(s_links_xml);  //loading from the xml to a list
        int id = Config.NextLinkId;
        Link copy = item with { LinkID = id };
        links.Add(copy);    //adding the new item to the list
        XMLTools.SaveListToXMLSerializer<Link>(links, s_links_xml); //saving the updated list in the xml
        return id;
    }

    public void Delete(int id)
    {
        Link? temp = Read(id);
        if (temp == null)
            throw new DalDoesNotExistException($"Link with ID={id} does Not exist");
        List<Link> links = new List<Link>();
        links = XMLTools.LoadListFromXMLSerializer<Link>(s_links_xml);
        links.Remove(temp); //deleting the item from the list
        XMLTools.SaveListToXMLSerializer(links, s_links_xml);   //saving the updated list to the xml
    }

    public Link? Read(Func<Link, bool> filter)
    {
        List<Link> links = XMLTools.LoadListFromXMLSerializer<Link>(s_links_xml);
        foreach (Link item in links)
            if (filter(item))
                return item;

        return null;    //didn't find
    }

    public Link? Read(int id)
    {
        List<Link> links = XMLTools.LoadListFromXMLSerializer<Link>(s_links_xml);
        foreach (Link item in links)
            if (item.LinkID == id)
                return item;
        return null;
    }

    public IEnumerable<Link?> ReadAll(Func<Link, bool>? filter = null)
    {
        List<Link> links = XMLTools.LoadListFromXMLSerializer<Link>(s_links_xml);
        if (filter != null)
        {
            return from item in links
                   where filter(item)
                   select item;
        }

        return from item in links    //if there is no filter, returning the whole list
               select item;
    }

    public void Update(Link item)
    {
        List<Link> links = XMLTools.LoadListFromXMLSerializer<Link>(s_links_xml);
        Link? temp = Read(item.LinkID);
        if (temp == null)
            throw new DalDoesNotExistException($"Link with ID={item.LinkID} does Not exist");
        links.Remove(item);
        links.Add(temp);
        XMLTools.SaveListToXMLSerializer(links, s_links_xml);
    }
}
