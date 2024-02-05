using BlImplementation;

namespace BlApi;

internal class BL : IBl
{
    public IEngineer Engineer =>  new EngineerImplementation();

    public ITask Task => throw new NotImplementedException();

    public IMilestone MileStone => throw new NotImplementedException();
}
