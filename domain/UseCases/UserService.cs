using domain.Logic;
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
            return Result.Fail<User>(user.IsValid().Error);
        }

        if (_db.IsUserExists(user.PhoneNumber).Success)
        {
            return Result.Fail<User>("User already exists.");
        }
        
        var success = _db.Create(user);

        if (success.IsFailure)
        {
            return success;
        }

        return success;
    }

    public Result<User> Authorization(string phoneNumber, string password)
    {
        if (string.IsNullOrEmpty(phoneNumber))
        {
            return Result.Fail<User>("Null or empty PhoneNumber.");
        }

        if (string.IsNullOrEmpty(password))
        {
            return Result.Fail<User>("Null or empty Password.");
        }

        if (_db.IsUserExists(phoneNumber).IsFailure)
        {
            return Result.Fail<User>("User doesn't exists.");
        }

        var success = _db.GetUserByLogin(phoneNumber);

        if (success.IsFailure) {
          return success;
        }

        if (success.Value == null) {
          return Result.Fail<User>("Nullable reference.");
        }

        if (success.Value.Password != password) {
          return Result.Fail<User>("Wrong password.");
        }

        return success;
    }
}
