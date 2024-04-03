namespace DalApi;

public interface IDal
{
    ITask Task { get; }
    IEngineer Engineer { get; }
    ILink Link { get; }
    DateTime? StartDate { get; set; }
    //DateTime? FinishDate { get; set; }
    public void saveStartandFinishDatestoFile(string data_config_xml, string elemName, DateTime? toSave);
    public DateTime? getStartOrFinshDatesFromXml(string elemName);

}
