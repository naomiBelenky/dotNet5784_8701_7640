namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

internal class EngineerImplementation : IEngineer
{
    readonly string s_engineers_xml = "engineers";

    public int Create(Engineer item)
    {
        if (Read(item.EngineerID) is not null) //If this id already exist
            throw new DalAlreadyExistsException($"Engineer with ID={item.EngineerID} already exist!!");

        XMLTools.SaveListToXMLElement();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        throw new NotImplementedException();
    }

    public Engineer? Read(int id)
    {
        XElement? engineerList = XMLTools.LoadListFromXMLElement(s_engineers_xml).Elements().FirstOrDefault(item => (int?)item.Element("EngineerID")==id);
        Engineer? newEngineer;
        try {
            newEngineer = (from p in engineerList.Elements()
                           where Convert.ToInt32(p.Element("EngineerID").Value)==id
                           select new Engineer()
                           {
                               EngineerID = Convert.ToInt32(p.Element("EngineerID").Value),
                               FullName = p.Element("Name").Value,
                               Email = p.Element("Email").Value,
                               Level = (EngineerLevel)Convert.ToInt32(p.Element("Level").Value),
                               CostPerHour = Convert.ToDouble(p.Element("CostPerHour").Value)
                           }).FirstOrDefault();
        }
        catch { newEngineer = null; }
        return newEngineer is null ? null : newEngineer;
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(Engineer item)
    {
        throw new NotImplementedException();
    }
}
