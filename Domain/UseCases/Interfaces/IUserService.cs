using Domain.Logic;
using Domain.Models;

namespace Domain.UseCases.Interfaces;

public interface IUserService
{
    Result<User> Registration(User user);
    Result<User> Authorization(string phoneNumber, string password);
}
