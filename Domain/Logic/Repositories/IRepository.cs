namespace Domain.Logic.Repositories;

public interface IRepository<T> where T : class
{
    T? Get(int id);
    T Create(T item);
    T Update(T item);
    T Delete(int id);
    void Save();
}
