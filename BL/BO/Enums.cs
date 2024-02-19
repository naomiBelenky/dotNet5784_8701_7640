namespace BO;

public enum Level
{
    Beginner,
    AdvancedBeginner,
    Intermediate,
    Advanced,
    Expert,
    All

}

public enum Status
{
    Unscheduled,
    Scheduled,
    OnTrack,
    //InJeopardy, (if we are doing milestone)
    Done,
    All
}

public enum Stage
{
    Planning,
    //MidScheduling,
    Execution
}
