namespace Dal;
using DalApi;
using System;

sealed internal class DalList : IDal
{
    private DalList() { }

    public static IDal Instance { get; } = new DalList();

    public ITask Task => new TaskImplementation();
    public IEngineer Engineer => new EngineerImplementation();
    public ILink Link => new LinkImplementation();

    //public DateTime? StartDate { get => Instance.StartDate; set => Instance.StartDate=value; }
    private DateTime? _startDate = null; // backing field
    
    public DateTime? StartDate
    {
        get => _startDate;
        set
        {
            _startDate = value;
            //Instance.StartDate = value; // Update the value in the instance
        }
    }

    //public DateTime? FinishDate { get => Instance.FinishDate; set => Instance.FinishDate=value; }
    
    public void saveStartandFinishDatestoFile(string data_config_xml, string elemName, DateTime? toSave) {  }

    public DateTime? getStartOrFinshDatesFromXml(string elemName) { return null; }

}
