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

    public void Create(Role item)
    {
        _context.Roles.Add(item.ToModel());
        Save();
    }

    public void Update(Role item)
    {
        _context.Roles.Update(item.ToModel());
        Save();
    }

    public void Delete(int id)
    {
        var role = _context.Roles.AsNoTracking().First(r => r.Id == id);
        _context.Remove(role);
        Save();
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}
