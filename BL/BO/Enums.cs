﻿namespace BO;

public enum Level
{
    Beginner,
    AdvancedBeginner,
    Intermediate,
    Advanced,
    Expert
}

public enum Status
{
    Unscheduled,
    Scheduled,
    OnTrack,
    //InJeopardy, (if we are doing milestone)
    Done
}

public enum Stage
{
    Planning,
    //MidScheduling,
    Execution
}
