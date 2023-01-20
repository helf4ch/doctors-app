using Database.Models;
using Domain.Models;

namespace Database.Converters;

public static class DomainModelRoleConverter
{
    public static RoleModel ToModel(this Role role)
    {
        return new RoleModel { Id = role.Id, Name = role.Name };
    }

    public static Role ToDomain(this RoleModel role)
    {
        return new Role { Id = role.Id, Name = role.Name };
    }
}
