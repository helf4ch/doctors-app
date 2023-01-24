using Domain.Logic;
using Domain.Logic.Repositories;
using Domain.Models;

namespace Domain.UseCases;

public class RoleService
{
    private IRoleRepository _db;

    public RoleService(IRoleRepository db)
    {
        _db = db;
    }

    public async Task<Result<Role>> GetRole(int id)
    {
        if (id == 0)
        {
            return Result.Fail<Role>("RoleService.GetRole: Invalid id.");
        }

        try
        {
            var success = await _db.Get(id);

            if (success is null)
            {
                return Result.Fail<Role>("RoleService.GetRole: Role doesn't exist.");
            }

            return Result.Ok<Role>(success);
        }
        catch (Exception ex)
        {
            return Result.Fail<Role>("RoleService.GetRole: " + ex.Message);
        }
    }

    public async Task<Result<Role>> CreateRole(Role role)
    {
        if (role.IsValid().IsFailure)
        {
            return Result.Fail<Role>("RoleService.CreateRole: " + role.IsValid().Error);
        }

        try
        {
            var success = await _db.Create(role);

            return Result.Ok<Role>(success);
        }
        catch (Exception ex)
        {
            return Result.Fail<Role>("RoleService.CreateRole: " + ex.Message);
        }
    }

    public async Task<Result<Role>> UpdateRole(Role role)
    {
        if (role.IsValid().IsFailure)
        {
            return Result.Fail<Role>("RoleService.UpdateRole: " + role.IsValid().Error);
        }

        try
        {
            var success = await _db.Update(role);

            return Result.Ok<Role>(success);
        }
        catch (Exception ex)
        {
            return Result.Fail<Role>("RoleService.UpdateRole: " + ex.Message);
        }
    }

    public async Task<Result<Role>> DeleteRole(int id)
    {
        if (id == 0)
        {
            return Result.Fail<Role>("RoleService.DeleteRole: Invalid id.");
        }

        try
        {
            var success = await _db.Delete(id);

            return Result.Ok<Role>(success);
        }
        catch (Exception ex)
        {
            return Result.Fail<Role>("RoleService.DeleteRole: " + ex.Message);
        }
    }
}
