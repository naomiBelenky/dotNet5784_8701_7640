using DalApi;
using DO;
using System.Xml.Linq;

namespace Dal;

sealed internal class DalXml : IDal
{
    private DalXml() { }
    public static IDal Instance { get; } = new DalXml();
    public ITask Task => new TaskImplementation();

    public IEngineer Engineer => new EngineerImplementation();

    public ILink Link => new LinkImplementation();

    public DateTime? StartDate { get => getStartOrFinshDatesFromXml("startDate");
        set => saveStartandFinishDatestoFile("data-config", "startDate", value); }
    //public DateTime? FinishDate { get => Instance.FinishDate; set => Instance.FinishDate=value; }

    public void saveStartandFinishDatestoFile(string data_config_xml, string elemName, DateTime? elemValue)
    {
        XElement root = XMLTools.LoadListFromXMLElement(data_config_xml);
        DateTime? date = root.ToDateTimeNullable(elemName);
        //checking if the date is already set
        if (date != null && elemValue != null) throw new DalAlreadyExistsException($"The date is already set");
        if (elemValue == null)
            root.Element(elemName)?.SetValue("");
        else
            root.Element(elemName)?.SetValue(elemValue);

        XMLTools.SaveListToXMLElement(root, data_config_xml);
    }

    public DateTime? getStartOrFinshDatesFromXml(string elemName)
    {
        XElement root = XMLTools.LoadListFromXMLElement("data-config");
        return( root.ToDateTimeNullable(elemName));
    }
}
