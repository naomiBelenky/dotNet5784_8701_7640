

using System.Globalization;

namespace DO;

/// <summary>
/// Task entity represents a task with all props.
/// </summary>
/// <param name="TaskID">Personal unique ID of the task</param>
/// <param name="TaskName">The task's name</param>
/// <param name="Description">The task's description</param>
/// <param name="Difficulty">The level of difficulty of the task, defines the level of expertise needed to work on it</param>
/// <param name="Milestone">Describes wether the task is complete</param>
/// <param name="Creation">The date that the task was created by the manager</param>
/// <param name="PlanToStart">The planned date to start working on the task</param>
/// <param name="StartWork">The actual date of starting working on the task</param>
/// <param name="TimeForTask">Amount of days that are needed to work on the task</param>
/// <param name="Deadline">Deadline of the task</param>
/// <param name="FinishDate">Actual date of finishing the task</param>
/// <param name="Product">Describes the results or provided items of the finished task</param>
/// <param name="Notes">Remarks and notes about the task or products</param>
/// <param name="EngineerId">The ID of the assigned engineer for the task</param>

public record Task
(
    int TaskID,
    string Name,
    string Description,
    Level Difficulty,
    bool Milestone = false,
    DateTime? Creation = null,
    DateTime? PlanToStart = null,
    DateTime? StartWork = null,
    TimeSpan? TimeForTask = null,
    DateTime? Deadline = null,
    DateTime? FinishDate = null,
    string? Product = null,
    string? Notes = null,
    int? EngineerID = 0
)
{
    public Task() : this(0, "", "", Level.Beginner) { }
}


