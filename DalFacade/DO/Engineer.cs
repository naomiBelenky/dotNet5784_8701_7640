
namespace DO;

/// <summary>
/// Engineer Entity represents a engineer with all its props
/// </summary>
/// <param name="EngineerID"> Personal unique ID of the engineer </param>
/// <param name="FullName"> The engineer's name </param>
/// <param name="Email"> The engineer's email </param>
/// <param name="Level"> The engineer's level </param>
/// <param name="CostPerHour"> The engineer's cost per hour </param>
public record Engineer
(
    int EngineerID,
    string FullName,
    string Email,
    Level Level,
    double CostPerHour
)
{
    public Engineer() : this(0, "", "", Level.Beginner, 0.0) { }
}
