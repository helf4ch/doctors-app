using Database.Converters;
using Domain.Logic.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationContext _context;

    public UserRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<User?> Get(int id)
    {
        var result = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

        return result?.ToDomain();
    }

    public async Task<User> Create(User item)
    {
        var model = item.ToModel();

        await _context.Users.AddAsync(model);
        await Save();

        return model.ToDomain();
    }

    public async Task<User> Update(User item)
    {
        var model = item.ToModel();

        _context.Users.Update(model);
        await Save();

        return model.ToDomain();
    }

    public async Task<User> Delete(int id)
    {
        var user = await _context.Users.AsNoTracking().FirstAsync(u => u.Id == id);

        _context.Users.Remove(user);
        await Save();

        return user.ToDomain();
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetByPhoneNumber(string phoneNumber)
    {
        var result = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);

        return result?.ToDomain();
    }
}
