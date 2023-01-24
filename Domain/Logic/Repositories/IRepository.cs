namespace Domain.Logic.Repositories;

public interface IRepository<T> where T : class
{
    Task<T?> Get(int id);
    Task<T> Create(T item);
    Task<T> Update(T item);
    Task<T> Delete(int id);
    Task Save();
}
