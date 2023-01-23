using Domain.Models;

namespace Domain.Logic.Repositories;

public interface IUserRepository : IRepository<User>
{
    User? GetByPhoneNumber(string phoneNumber);
}
