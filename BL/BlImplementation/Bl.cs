namespace BlImplementation;
using BlApi;
internal class Bl : IBl
{
    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();

    //public IMilestone MileStone => throw new NotImplementedException();
}
