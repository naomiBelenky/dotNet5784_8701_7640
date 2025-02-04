﻿namespace BlImplementation;
using BlApi;
using BO;
using Dal;
using System.Xml.Linq;

internal class Bl : IBl
{
    public IEngineer Engineer => new EngineerImplementation(this);

    public ITask Task => new TaskImplementation(this);

    public IMilestone MileStone => throw new NotImplementedException();

    private static DateTime s_Clock = DateTime.Now.Date;
    public DateTime Clock { get { return s_Clock; } private set { s_Clock = value; } }

    public DateTime InitTime()
    {
        Clock = DateTime.Now;
        return Clock;
    }

    public void PromoteHour()
    {
        Clock = Clock.AddHours(1);
    }

    public void PromoteDay()
    {
        Clock = Clock.AddDays(1);
    }

    public void PromoteYear()
    {
        Clock = Clock.AddYears(1);
    }

    public BO.Stage StageOfProject { get => StageOfProject; set => StageOfProject = value; }

    public void InitializeDB() => DalTest.Initialization.Do();
    public void ResetDB() => DalTest.Initialization.Reset();

    public void saveStartDate(DateTime date)
    {
        //DalApi.Factory.Get.saveStartandFinishDatestoFile("data-config", "startDate", date);
        DalApi.Factory.Get.StartDate = date;
    }
    public DateTime? getStartDate()
    {
        //return (DalApi.Factory.Get.getStartOrFinshDatesFromXml("startDate"));
        return DalApi.Factory.Get.StartDate;
    }

    public Stage getStage()
    {
        //XElement root = XMLTools.LoadListFromXMLElement("data-config");
        //DateTime? date = root.ToDateTimeNullable("startDate");
        DateTime? date = DalApi.Factory.Get.StartDate;

        if (date == null || date.ToString() == "")
            return Stage.Planning;
        else return Stage.Execution;
    }

    public void recursiveSchedule(int id)
    {
        BO.Task fullTask = Task.Read(id) ?? throw new BO.BlDoesNotExistException($"Task with ID={id} does not exist");
        if (fullTask.PlanToStart != null) return;   //if the starting date is already set

        List<TaskInList>? prevTasks = fullTask.Links?.ToList() ?? new List<TaskInList>();   //if there are no links, creating an empty list
        foreach (var task in prevTasks)
        {
            recursiveSchedule(task.Id);
        }
        DateTime planToStart = (DateTime)Task.SuggestStartDate(id)!;
        Task.UpdateDate(id, planToStart);
    }

    public void automaticSchedule()
    {
        //StageOfProject = BO.Stage.Execution;

        List<BO.TaskInList> tasks = (Task.ReadAll()).ToList();

        foreach (var task in tasks)
        {
            recursiveSchedule(task.Id);
        }
    }


}
