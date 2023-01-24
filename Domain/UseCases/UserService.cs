using Domain.Logic;
using Domain.Logic.Repositories;
using Domain.Models;

namespace Domain.UseCases;

public class UserService
{
    private IUserRepository _db;

    public UserService(IUserRepository db)
    {
        _db = db;
    }

    public async Task<Result<User>> GetUser(int id)
    {
        if (id == 0)
        {
            return Result.Fail<User>("UserService.GetUser: Invalid id.");
        }

        try
        {
            var success = await _db.Get(id);

            if (success is null)
            {
                return Result.Fail<User>("UserService.GetUser: User doesn't exist.");
            }

            return Result.Ok<User>(success);
        }
        catch (Exception ex)
        {
            return Result.Fail<User>("UserService.GetUser: " + ex.Message);
        }
    }

    public async Task<Result<User>> Authorization(string phoneNumber, string password)
    {
        if (string.IsNullOrEmpty(phoneNumber))
        {
            return Result.Fail<User>("UserService.Authorization: Null or empty PhoneNumber.");
        }

        if (string.IsNullOrEmpty(password))
        {
            return Result.Fail<User>("UserService.Authorization: Null or empty Password.");
        }

        try
        {
            var success = await _db.GetByPhoneNumber(phoneNumber);

            if (success is null)
            {
                return Result.Fail<User>("UserService.Authorization: User doesn't exist.");
            }

            var generatedPassword = User.GeneratePassword(password, success.Salt!);

            if (success.Password != generatedPassword)
            {
                return Result.Fail<User>("UserService.Authorization: Wrong Password.");
            }

            return Result.Ok<User>(success);
        }
        catch (Exception ex)
        {
            return Result.Fail<User>("UserService.Authorization: " + ex.Message);
        }
    }

    public async Task<Result<User>> Registration(User user)
    {
        user.Salt = User.GenerateSalt();

        if (user.IsValid().IsFailure)
        {
            return Result.Fail<User>("UserService.Registration: " + user.IsValid().Error);
        }

        try
        {
            var item = await _db.GetByPhoneNumber(user.PhoneNumber!);

            if (item is not null)
            {
                return Result.Fail<User>("UserService.Registration: PhoneNumber is already taken.");
            }

            user.Password = User.GeneratePassword(user.Password!, user.Salt);

            var success = await _db.Create(user);

            return Result.Ok<User>(success);
        }
        catch (Exception ex)
        {
            return Result.Fail<User>("UserService.Registration: " + ex.Message);
        }
    }

    public async Task<Result<User>> UpdateUser(User user)
    {
        if (user.IsValid().IsFailure)
        {
            return Result.Fail<User>("UserService.UpdateUser: " + user.IsValid().Error);
        }

        try
        {
            var success = await _db.Update(user);

            return Result.Ok<User>(success);
        }
        catch (Exception ex)
        {
            return Result.Fail<User>("UserService.UpdateUser: " + ex.Message);
        }
    }

    public async Task<Result<User>> DeleteUser(int id)
    {
        if (id == 0)
        {
            return Result.Fail<User>("UserService.DeleteUser: Invalid id.");
        }

        try
        {
            var success = await _db.Delete(id);

            return Result.Ok<User>(success);
        }
        catch (Exception ex)
        {
            return Result.Fail<User>("UserService.DeleteUser: " + ex.Message);
        }
    }
}
