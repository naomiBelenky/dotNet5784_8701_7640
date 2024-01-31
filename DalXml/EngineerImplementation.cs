namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Security.Cryptography;
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
                          where Convert.ToInt32(p.Element("EngineerID").Value) == id
                          select p).FirstOrDefault();

        if (temp != null)
            temp.Remove();

        XMLTools.SaveListToXMLElement(engineerList, s_engineers_xml);
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        //XElement? temp = XMLTools.LoadListFromXMLElement(s_engineers_xml).Elements().FirstOrDefault(filter);

        return XMLTools.LoadListFromXMLElement(s_engineers_xml).Elements().Select(eng => getEngineer(eng)).FirstOrDefault(filter);
    }

    public Engineer? Read(int id)
    {
        XElement? engineer = XMLTools.LoadListFromXMLElement(s_engineers_xml).Elements().FirstOrDefault(item => (int?)item.Element("EngineerID") == id);
        return engineer is null ? null : getEngineer(engineer);
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        if (filter != null)
            return XMLTools.LoadListFromXMLElement(s_engineers_xml).Elements().Select(eng => getEngineer(eng)).Where(filter);
        else
            return XMLTools.LoadListFromXMLElement(s_engineers_xml).Elements().Select(eng => getEngineer(eng));
    }

    public void Update(Engineer item)
    {
        if (Read(item.EngineerID) == null) //If this id doesnt exist
            throw new DalDoesNotExistException($"Engineer with ID={item.EngineerID} does Not exist");

        XElement engineerList = XMLTools.LoadListFromXMLElement(s_engineers_xml);
        Delete(item.EngineerID);
        Create(item);
    }

    public Engineer getEngineer(XElement eng)
    {
        return new Engineer()
        {
            EngineerID = Convert.ToInt32(eng.Element("EngineerID")!.Value),
            FullName = eng.Element("FullName")!.Value,
            Email = eng.Element("Email")!.Value,
            Level = Enum.Parse<Level>(eng.Element("Level")!.Value),
            CostPerHour = Convert.ToDouble(eng.Element("CostPerHour")!.Value)
        };
    }

    public void DeleteAll()  //delete all the antity objects in case of new initialization
    {

        XElement engineerList = XMLTools.LoadListFromXMLElement(s_engineers_xml);
        engineerList.RemoveAll();


        //foreach (var item in s_engineers_xml)
        //    Delete(item);

        XMLTools.SaveListToXMLElement(engineerList, s_engineers_xml);
    }
}
