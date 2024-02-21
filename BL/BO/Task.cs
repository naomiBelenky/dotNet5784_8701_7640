using BlApi;
using DO;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BO;

/// <summary>
/// Task entity represents a task with all props.
/// </summary>

public class Task
{
    /// <summary>
    /// Personal unique ID of the task
    /// </summary>
    public int Id { get; init; }
    /// <summary>
    /// The task's name
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// The task's description
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// The date that the task was created by the manager
    /// </summary>
    public DateTime Creation { get; init; }
    /// <summary>
    /// The status of the task (unscheduled, done...)
    /// </summary>
    public BO.Status? Status { get; set; }
    /// <summary>
    /// A list of the tasks that this task depends on
    /// </summary>
    public List<BO.TaskInList>? Links { get; set; }
    /// <summary>
    /// The planned date to start working on the task
    /// </summary>
    public DateTime? PlanToStart { get; set; }
    /// <summary>
    /// The actual date of starting working on the task
    /// </summary>
    public DateTime? StartWork { get; set; }
    /// <summary>
    /// The date that is planned to finish the task
    /// </summary>
    public DateTime? PlanToFinish { get; set; }
    /// <summary>
    /// Deadline of the task
    /// </summary>
    public DateTime? Deadline { get; set; }
    /// <summary>
    /// Actual date of finishing the task
    /// </summary>
    public DateTime? FinishDate { get; set; }
    /// <summary>
    /// Amount of days that are needed to work on the task
    /// </summary>
    public TimeSpan? Duration { get; set; }
    /// <summary>
    /// Describes the results or provided items of the finished task
    /// </summary>
    public string? Product { get; set; }
    /// <summary>
    /// Remarks and notes about the task or products
    /// </summary>
    public string? Notes { get; set; }
    /// <summary>
    /// The assigned engineer for the task
    /// </summary>
    public BO.EngineerInTask? Engineer { get; set; }
    /// <summary>
    /// The level of difficulty of the task, defines the level of expertise needed to work on it
    /// </summary>
    public BO.Level Difficulty { get; set; }

    public override string ToString() => Tools.ToStringProperty(this);
    //public override string ToString()
    //{
    //    return $" Id: {Id}\n Name: {Name}\n Description: {Description}\n Creation: {Creation}\n Status: {Status}\n PlanToStart: {PlanToStart}\n StartWork: {StartWork}\n PlanToFinish: {PlanToFinish}\n Deadline: {Deadline}\n FinishDate: {FinishDate}\n Duration: {Duration}\n Product: {Product}\n Notes: {Notes}\n Engineer: {Engineer}\n Difficulty: {Difficulty}\n Dependencies: {Links}";
    //}   //i didn't print Links
}
