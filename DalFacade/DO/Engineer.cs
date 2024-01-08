namespace DO;

/// <summary>
/// Engineer entity represents an engineer with all its props.
/// </summary>
/// <param name="EngineerID">Personal unique ID of the engineer</param>
/// <param name="FullName">The engineer's full name</param>
/// <param name="Email">The engineer's email adress</param>
/// <param name="Level">The engineer's level of experience</param>
/// <param name="CostPerHour">The engineer's cost per hour</param>
public record Engineer
(
    int EngineerID,
    string FullName,
    string Email,
    EngineerLevel Level,    //nullable?
    double? CostPerHour = null
)
{
    public Engineer() : this(0, "", "", EngineerLevel.Beginner) { }
}
