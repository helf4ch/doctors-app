using Database.Converters;
using Domain.Logic.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly ApplicationContext _context;

    public RoleRepository(ApplicationContext context)
    {
        _context = context;
    }

    public Role? Get(int id)
    {
        var result = _context.Roles.FirstOrDefault(r => r.Id == id)?.ToDomain();

        return result;
    }

    public Role Create(Role item)
    {
        var model = item.ToModel();

        _context.Roles.Add(model);
        Save();

        return model.ToDomain();
    }

    public Role Update(Role item)
    {
        var model = item.ToModel();

        _context.Roles.Update(model);
        Save();

        return model.ToDomain();
    }

    public Role Delete(int id)
    {
        var role = _context.Roles.AsNoTracking().First(r => r.Id == id);

        _context.Remove(role);
        Save();

        return role.ToDomain();
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}
