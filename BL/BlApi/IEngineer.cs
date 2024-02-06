

namespace BlApi;

public interface IEngineer
{
    /// <summary>
    /// Returns all engineers According to the condition of the function. If no function is null returns them all
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<BO.Engineer> ReadAll(Func<bool>? filter = null); //Funk needs to get paramater?
    /// <summary>
    /// gets id and returns its engineer (throws exeption if doesnt find)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.Engineer? Read(int id);
    /// <summary>
    /// gets a engineer and add it to the data.
    /// </summary>
    /// <param name="engineer"></param>
    public int Add( BO.Engineer engineer);
    /// <summary>
    /// gets a engineer and updates the current engineer (throws exeption if doesnt find)
    /// </summary>
    /// <param name="id"></param>
    public void Delete(int id);
    /// <summary>
    /// gets engineer id and deletes it from the data. (throws exeption if doesnt find)
    /// </summary>
    /// <param name="engineer"></param>
    public void Update(BO.Engineer engineer);

}
