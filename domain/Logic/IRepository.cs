using domain.Models;

namespace domain.Logic;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    Result<T> GetItem(int id);
    Result<T> Create(T item);
    Result<T> Update(T item);
    Result Delete(int id);
    Result<T> Save();
}

public interface IUserRepository : IRepository<User>
{
    Result IsUserExists(string phoneNumber);
    Result<User> GetUserByLogin(string phoneNumber);
}
