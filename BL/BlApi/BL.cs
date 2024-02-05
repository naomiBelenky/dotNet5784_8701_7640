using BlImplementation;

namespace BlApi;

internal class BL : IBl
{
    public IEngineer Engineer =>  new EngineerImplementation();

    public ITask Task => new TaskImplementation();

    public IMilestone MileStone => throw new NotImplementedException();
}
