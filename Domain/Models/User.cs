using Domain.Logic;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Models;

public class User
{
    public int Id { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Name { get; set; }
    public string? Secondname { get; set; }
    public string? Surname { get; set; }
    public int RoleId { get; set; }
    public string? Password { get; set; }
    public string? Salt { get; set; }

    public Result IsValid()
    {
        if (string.IsNullOrEmpty(PhoneNumber))
        {
            return Result.Fail("User.IsValid: Null or empty PhoneNumber.");
        }

        if (PhoneNumber.Length > 15)
        {
            return Result.Fail("User.IsValid: PhoneNumber has MaxLenght of 15.");
        }

        if (string.IsNullOrEmpty(Name))
        {
            return Result.Fail("User.IsValid: Null or empty Name.");
        }

        if (Name.Length > 50)
        {
            return Result.Fail("User.IsValid: Name has MaxLenght of 50.");
        }

        if (Secondname?.Length > 50)
        {
            return Result.Fail("User.IsValid: Secondname has MaxLenght of 50.");
        }

        if (Surname?.Length > 50)
        {
            return Result.Fail("User.IsValid: Surname has MaxLenght of 50.");
        }

        if (RoleId == 0)
        {
            return Result.Fail("User.IsValid: RoleId is invalid.");
        }

        if (string.IsNullOrEmpty(Password))
        {
            return Result.Fail("User.IsValid: Null or empty Password.");
        }

        if (Password.Length > 64)
        {
            return Result.Fail("User.IsValid: Password has MaxLenght of 64.");
        }

        if (string.IsNullOrEmpty(Salt))
        {
            return Result.Fail("User.IsValid: Null or empty Salt.");
        }

        if (Salt.Length > 32)
        {
            return Result.Fail("User.IsValid: Salt has MaxLenght of 32.");
        }

        return Result.Ok();
    }

    public static string GenerateSalt()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(128 / 8));
    }

    public static string GeneratePassword(string password, string salt)
    {
        return Convert.ToBase64String(
            KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.ASCII.GetBytes(salt),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 256,
                numBytesRequested: 256 / 6
            )
        );
    }
}
