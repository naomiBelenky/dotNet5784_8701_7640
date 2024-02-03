using BlImplementation;

namespace BlApi;

internal class BL : IBL
{
    public IEngineer Engineer =>  new EngineerImplementation();

    public ITask Task => throw new NotImplementedException();

    public IMilestone MileStone => throw new NotImplementedException();
}
