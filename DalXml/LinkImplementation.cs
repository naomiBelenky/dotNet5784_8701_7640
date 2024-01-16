

namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;

internal class LinkImplementation:ILink
{
    readonly string s_links_xml = "links";

    public int Create(Link item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Link? Read(Func<Link, bool> filter)
    {
        throw new NotImplementedException();
    }

    public Link? Read(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Link?> ReadAll(Func<Link, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(Link item)
    {
        throw new NotImplementedException();
    }
}
