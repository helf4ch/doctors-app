using domain.Logic;
using domain.Models;

namespace domain.UseCases.Interfaces;

public interface IUserService
{
    Result<User> Registration(User user);
    Result<User> Authorization(string phoneNumber, string password);
}
