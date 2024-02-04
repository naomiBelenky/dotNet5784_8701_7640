using BO;

namespace BlApi;

public interface IBl
{
    public IEngineer Engineer { get; }
    public ITask Task { get; }
    public IMilestone MileStone { get; }
    static Stage Stage { get;  }
}
