namespace domain.Logic.Repositories;

public interface IRepository<T> where T : class
{
    Result IsExists(int id);
    Result<T> GetItem(int id);
    Result<T> Create(T item);
    Result<T> Update(T item);
    Result Delete(int id);
    Result<T> Save();
}
