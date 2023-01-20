using Domain.Logic;

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

        if (string.IsNullOrEmpty(Name))
        {
            return Result.Fail("User.IsValid: Null or empty Name.");
        }

        if (string.IsNullOrEmpty(Password))
        {
            return Result.Fail("User.IsValid: Null or empty Password.");
        }

        if (string.IsNullOrEmpty(Salt))
        {
            return Result.Fail("User.IsValid: Null or empty Salt.");
        }

        return Result.Ok();
    }
}
