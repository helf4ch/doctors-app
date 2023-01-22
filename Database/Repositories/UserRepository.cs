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

    public User? Get(int id)
    {
        var result = _context.Users.AsNoTracking().FirstOrDefault(u => u.Id == id)?.ToDomain();

        return result;
    }

    public User Create(User item)
    {
        var model = item.ToModel();

        _context.Users.Add(model);
        Save();

        return model.ToDomain();
    }

    public User Update(User item)
    {
        var model = item.ToModel();

        _context.Users.Update(model);
        Save();

        return model.ToDomain();
    }

    public User Delete(int id)
    {
        var user = _context.Users.AsNoTracking().First(u => u.Id == id);

        _context.Users.Remove(user);
        Save();

        return user.ToDomain();
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public User? GetByPhoneNumber(string phoneNumber)
    {
        var result = _context.Users
            .AsNoTracking()
            .FirstOrDefault(u => u.PhoneNumber == phoneNumber)
            ?.ToDomain();

        return result;
    }
}
