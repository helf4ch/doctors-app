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
        var result = _context.Users.FirstOrDefault(u => u.Id == id)?.ToDomain();

        return result;
    }

    public void Create(User item)
    {
        _context.Users.Add(item.ToModel());
        Save();
    }

    public void Update(User item)
    {
        _context.Users.Update(item.ToModel());
        Save();
    }

    public void Delete(int id)
    {
        var user = _context.Users.AsNoTracking().First(u => u.Id == id);
        _context.Users.Remove(user);
        Save();
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public User? Get(string phoneNumber)
    {
        var result = _context.Users.FirstOrDefault(u => u.PhoneNumber == phoneNumber)?.ToDomain();

        return result;
    }
}
