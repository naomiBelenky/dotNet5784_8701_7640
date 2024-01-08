

namespace DO;

/// <summary>
/// Link Entity links between interdependent tasks
/// </summary>
/// <param name="LinkID"> Personal unique ID of the Link </param>
/// <param name="PrevTask"> The previous task required for the next task </param>
/// <param name="NextTask"> The next task that awaits the previous task </param>
public record Link
(
    int LinkID,
    int PrevTask, //nullable??
    int NextTask //nullable??
)
{
    public Link() : this(0, 0, 0) { } //empty ctor for stage 3
    //how create parameter ctor? 
}


