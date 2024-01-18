using DalApi;

namespace Dal;

public class DalXml : IDal
{
    public ITask Task => new TaskImplementation();

    public IEngineer Engineer => new EngineerImplementation();

    public ILink Link => new LinkImplementation();
}
