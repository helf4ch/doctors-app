using domain.Logic;
using domain.Logic.Repositories;
using domain.Models;

namespace domain.UseCases;

public class UserService
{
    private IUserRepository _db;

    public UserService(IUserRepository db)
    {
        _db = db;
    }

    public Result<User> Registration(User user)
    {
        if (user.IsValid().IsFailure)
        {
            return Result.Fail<User>("UserService.Registration: " + user.IsValid().Error);
        }

        if (_db.IsPhoneTaken(user.PhoneNumber).Success)
        {
            return Result.Fail<User>("UserService.Registration: PhoneNumber is already taken.");
        }

        var success = _db.Create(user);

        if (success.IsFailure)
        {
            return Result.Fail<User>("UserService.Registration: " + success.Error);
        }

        return success;
    }

    public Result<User> Authorization(string phoneNumber, string password)
    {
        if (string.IsNullOrEmpty(phoneNumber))
        {
            return Result.Fail<User>("UserService.Authorization: Null or empty PhoneNumber.");
        }

        if (string.IsNullOrEmpty(password))
        {
            return Result.Fail<User>("UserService.Authorization: Null or empty Password.");
        }

        if (_db.IsPhoneTaken(phoneNumber).IsFailure)
        {
            return Result.Fail<User>("UserService.Authorization: User doesn't exists.");
        }

        var success = _db.GetUserByLogin(phoneNumber);

        if (success.IsFailure)
        {
            return Result.Fail<User>("UserService.Authorization: " + success.Error);
        }

        if (success.Value == null)
        {
            return Result.Fail<User>("UserService.Authorization: Nullable reference.");
        }

        if (success.Value.Password != password)
        {
            return Result.Fail<User>("UserService.Authorization: Wrong password.");
        }

        return success;
    }
}
