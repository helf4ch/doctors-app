using Domain.Models;

namespace Domain.Logic.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByPhoneNumber(string phoneNumber);
}
