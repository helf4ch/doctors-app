using Database.Models;
using Domain.Models;

namespace Database.Converters;

public class DomainModelRoleConverter
{
    public static RoleModel ToModel(Role role)
    {
        return new RoleModel { Id = role.Id, Name = role.Name };
    }

    public static Role ToDomain(RoleModel role)
    {
        return new Role { Id = role.Id, Name = role.Name };
    }
}
