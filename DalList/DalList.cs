namespace Dal;
using DalApi;
sealed public class DalList : IDal
{
    public ITask Task => new TaskImplementation();

    public IEngineer Engineer => new EngineerImplementation();

    public ILink Link => new LinkImplementation();
}
