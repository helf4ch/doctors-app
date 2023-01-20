using Database.Models;
using Domain.Models;

namespace Database.Converters;

public class DomainModelUserConverter
{
    public static UserModel ToModel(User user)
    {
        return new UserModel
        {
            Id = user.Id,
            PhoneNumber = user.PhoneNumber,
            Name = user.Name,
            Secondname = user.Secondname,
            Surname = user.Surname,
            RoleId = user.RoleId,
            Password = user.Password,
            Salt = user.Salt
        };
    }

    public static User ToDomain(UserModel user)
    {
        return new User
        {
            Id = user.Id,
            PhoneNumber = user.PhoneNumber,
            Name = user.Name,
            Secondname = user.Secondname,
            Surname = user.Surname,
            RoleId = user.RoleId,
            Password = user.Password,
            Salt = user.Salt
        };
    }
}
