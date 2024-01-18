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

        XElement engineerList = XMLTools.LoadListFromXMLElement(s_engineers_xml);

        XElement newItem = new XElement("Engineer");

        newItem.Add(new XElement("EngineerID", item.EngineerID));
        newItem.Add(new XElement("FullName", item.FullName));
        newItem.Add(new XElement("Email", item.Email));
        newItem.Add(new XElement("Level", item.Level));
        if (item.CostPerHour!=null)
            newItem.Add(new XElement("CostPerHour", item.CostPerHour));

        engineerList.Add(newItem);
        XMLTools.SaveListToXMLElement(engineerList, s_engineers_xml);

        return item.EngineerID;
    }

    public void Delete(int id)
    {
        //Engineer? temp = Read(id);
        if (Read(id) == null) //If this id doesnt exist
            throw new DalDoesNotExistException($"Engineer with ID={id} does Not exist");


        XElement engineerList = XMLTools.LoadListFromXMLElement(s_engineers_xml);

        XElement? temp = (from p in engineerList.Elements()
                          where Convert.ToInt32(p.Element("id").Value) == id
                          select p).FirstOrDefault();

        if (temp != null)
            temp.Remove();

        XMLTools.SaveListToXMLElement(engineerList, s_engineers_xml);
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        throw new NotImplementedException();
    }

    public Engineer? Read(int id)
    {
        XElement? engineer = XMLTools.LoadListFromXMLElement(s_engineers_xml).Elements().FirstOrDefault(item => (int?)item.Element("EngineerID") ==id);
        return engineer is null ? null : new Engineer()
        {
            EngineerID = Convert.ToInt32(engineer.Element("EngineerID").Value),
            FullName = engineer.Element("Name").Value,
            Email = engineer.Element("Email").Value,
            Level = (EngineerLevel)Convert.ToInt32(engineer.Element("Level").Value),
            CostPerHour = Convert.ToDouble(engineer.Element("CostPerHour").Value)
        };
        //return newEngineer is null ? null : newEngineer;
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(Engineer item)
    {
        Engineer? temp = Read(item.EngineerID);
        if (temp == null) //If this id doesnt exist
            throw new DalDoesNotExistException($"Engineer with ID={item.EngineerID} does Not exist");

        XElement engineerList = XMLTools.LoadListFromXMLElement(s_engineers_xml);
        Delete(item.EngineerID);
        Create(item);
    }
}
