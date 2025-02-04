﻿namespace BO;

/// <summary>
/// for the tasks list screen
/// </summary>

public class TaskInList
{
    /// <summary>
    /// Id of the task
    /// </summary>
    public int Id { get; init; }
    /// <summary>
    /// Name of the task
    /// </summary>
    public required string Name { get; init; }
    /// <summary>
    /// Description of the task
    /// </summary>
    public required string Description { get; set; }
    /// <summary>
    /// The status of the task (unscheduled, done...)
    /// </summary>
    public BO.Status? Status { get; set; }

    public override string ToString() => Tools.ToStringProperty(this);
    //public override string ToString()
    //{
    //    return ($" Id: {Id}\n Name: {Name}\n Description: {Description}\n Status: {Status}\n");
    //}
}
