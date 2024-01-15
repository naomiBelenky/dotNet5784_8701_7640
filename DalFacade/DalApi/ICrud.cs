using DO;

namespace DalApi;

public interface ICrud<T> where T : class
{
    int Create(T item); //Creates new entity object in DAL
    //T? Read(Func<T, bool> filter); //Reads entity object by its ID
    T? Read(int id); //Stage 1 only, Reads entity object by its ID 
    IEnumerable<T?> ReadAll(Func<T, bool>? filter = null); //Reads all entity objects
    //List<T> ReadAll(); //stage 1 only, Reads all entity objects //Stage 1
    void Update(T item); //Updates entity object
    void Delete(int id); //Deletes an object by its Id
}
