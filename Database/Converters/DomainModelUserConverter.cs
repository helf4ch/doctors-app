using Database.Models;
using Domain.Models;

namespace Database.Converters;

public static class DomainModelUserConverter
{
    public static UserModel ToModel(this User user)
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

    public static User ToDomain(this UserModel user)
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
