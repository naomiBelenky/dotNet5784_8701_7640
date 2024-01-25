using DalApi;

namespace Dal;

sealed public class DalXml : IDal
{
    public static IDal Instance { get; } = new DalXml();
    public ITask Task => new TaskImplementation();

    public IEngineer Engineer => new EngineerImplementation();

    public ILink Link => new LinkImplementation();
}
