using Database.Converters;
using Domain.Logic.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories;

public class SpecializationRepository : ISpecializationRepository
{
    private readonly ApplicationContext _context;

    public SpecializationRepository(ApplicationContext context)
    {
        _context = context;
    }

    public Specialization? Get(int id)
    {
        var result = _context.Specializations.FirstOrDefault(s => s.Id == id).ToDomain();

        return result;
    }

    public void Create(Specialization item)
    {
        _context.Specializations.Add(item.ToModel());
        Save();
    }

    public void Update(Specialization item)
    {
        _context.Specializations.Update(item.ToModel());
        Save();
    }

    public void Delete(int id)
    {
        var specialization = _context.Specializations.AsNoTracking().First(s => s.Id == id);
        _context.Specializations.Remove(specialization);
        Save();
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}
