
namespace DalApi;

using DO;
public interface ILink
{
    int Create(Link item); //Creates new entity object in DAL
    Link? Read(int id); //Reads entity object by its ID 
    List<Link> ReadAll(); //stage 1 only, Reads all entity objects
    void Update(Link item); //Updates entity object
    void Delete(int id); //Deletes an object by its Id
}
