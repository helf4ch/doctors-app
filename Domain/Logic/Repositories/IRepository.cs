namespace Domain.Logic.Repositories;

public interface IRepository<T> where T : class
{
    T? Get(int id);
    void Create(T item);
    void Update(T item);
    void Delete(int id);
    void Save();
}
