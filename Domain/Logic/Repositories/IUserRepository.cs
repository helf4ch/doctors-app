using Domain.Models;

namespace Domain.Logic.Repositories;

public interface IUserRepository : IRepository<User>
{
    Result IsPhoneTaken(string phoneNumber);
    Result<User> GetUserByLogin(string phoneNumber);
}
