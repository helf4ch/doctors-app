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

    public async Task<Role?> Get(int id)
    {
        var result = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);

        return result?.ToDomain();
    }

    public async Task<Role> Create(Role item)
    {
        var model = item.ToModel();

        await _context.Roles.AddAsync(model);
        await Save();

        return model.ToDomain();
    }

    public async Task<Role> Update(Role item)
    {
        var model = item.ToModel();

        _context.Roles.Update(model);
        await Save();

        return model.ToDomain();
    }

    public async Task<Role> Delete(int id)
    {
        var role = await _context.Roles.AsNoTracking().FirstAsync(r => r.Id == id);

        _context.Remove(role);
        await Save();

        return role.ToDomain();
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}
