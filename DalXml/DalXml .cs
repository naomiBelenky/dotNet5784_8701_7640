using DalApi;
using System.Xml.Linq;

namespace Dal;

sealed internal class DalXml : IDal
{
    public static IDal Instance { get; } = new DalXml();
    public ITask Task => new TaskImplementation();

    public IEngineer Engineer => new EngineerImplementation();

    public ILink Link => new LinkImplementation();

    //public DateTime? StartDate { get => Instance.StartDate; set => Instance.StartDate=value; }
    //public DateTime? FinishDate { get => Instance.FinishDate; set => Instance.FinishDate=value; }

    public bool saveStartandFinishDatestoFile(string data_config_xml, string elemName, DateTime elemValue)
    {
        XElement root = XMLTools.LoadListFromXMLElement(data_config_xml);
        DateTime? date = root.ToDateTimeNullable(elemName);
        //checking if the date is already set
        if (date != null) return false;
        root.Element(elemName)?.SetValue(elemValue);

        XMLTools.SaveListToXMLElement(root, data_config_xml);
        return true;
    }

    public DateTime? getStartOrFinshDatesFromXml(string elemName)
    {
        XElement root = XMLTools.LoadListFromXMLElement("data-config");
        return( root.ToDateTimeNullable(elemName));
    }
}
