namespace Dal;
using DalApi;
using System;

sealed internal class DalList : IDal
{
    public static IDal Instance { get; } = new DalList();
    public ITask Task => new TaskImplementation();

    public IEngineer Engineer => new EngineerImplementation();

    public ILink Link => new LinkImplementation();

    public DateTime? StartDate { get => Instance.StartDate; set => Instance.StartDate=value; }

    public DateTime? FinishDate { get => Instance.FinishDate; set => Instance.FinishDate=value; }

    private DalList() { }
}
