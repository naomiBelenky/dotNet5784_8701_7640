

using System.Globalization;

namespace DO;

/// <summary>
/// Task Entity represents a task with all its props
/// </summary>
/// <param name="TaskID"> Personal unique ID of the task </param>
/// <param name="Name"> The task's name </param>
/// <param name="Description"> The task's description </param>
/// <param name="Milestone"> Describes whether the task is complete </param>
/// <param name="Creation"> The date of the task's creation </param>
/// <param name="PlanToStart"> The date of starting according to plan  </param>
/// <param name="StartWork"> the date of starting work </param>
/// <param name="TimeForTask"> the ammount of days that needed for the task  </param>
/// <param name="Deadline"></param>
/// <param name="FinishDate"></param>
/// <param name="Product"></param>
/// <param name="Notes"></param>
/// <param name="EngineerID"></param>
/// <param name="Difficulty"></param>

public record Task
(
    int TaskID,
    string Name,
    string Description,
    bool Milestone = false,
    DateTime? Creation = null,
    DateTime? PlanToStart = null,
    DateTime? StartWork = null,
    double? TimeForTask = null,
    DateTime? Deadline = null,
    DateTime? FinishDate = null,
    string? Product = null,
    string? Notes = null,
    int EngineerID = 0,
    TaskDifficulty Difficulty = TaskDifficulty.Novice//null?
)
{
    public Task() : this(0, "", "") { }
}


