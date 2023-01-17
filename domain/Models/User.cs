using domain.Logic;

namespace domain.Models;

public class User
{
    public int Id { get; set; }
    public string PhoneNumber { get; set; }
    public string Name { get; set; }
    public string Secondname { get; set; }
    public string Surname { get; set; }
    public int RoleId { get; set; }
    public string Password { get; set; }

    public User(
        int id,
        string phoneNumber,
        string name,
        string secondname,
        string surname,
        int roleId,
        string password
    )
    {
        Id = id;
        PhoneNumber = phoneNumber;
        Name = name;
        Secondname = secondname;
        Surname = surname;
        RoleId = roleId;
        Password = password;
    }

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

        if (string.IsNullOrEmpty(Secondname))
        {
            return Result.Fail("User.IsValid: Null or empty Secondname.");
        }

        if (string.IsNullOrEmpty(Surname))
        {
            return Result.Fail("User.IsValid: Null or empty Surname.");
        }

        if (string.IsNullOrEmpty(Password))
        {
            return Result.Fail("User.IsValid: Null or empty Password.");
        }

        return Result.Ok();
    }
}
