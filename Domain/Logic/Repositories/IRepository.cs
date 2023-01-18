namespace Domain.Logic.Repositories;

public interface IRepository<T> where T : class
{
    Result<T> Get(int id);
    Result<T> Create(T item);
    Result<T> Update(T item);
    Result Delete(int id);
}
