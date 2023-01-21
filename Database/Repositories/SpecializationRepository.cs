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
        var result = _context.Specializations
            .AsNoTracking()
            .FirstOrDefault(s => s.Id == id)
            .ToDomain();

        return result;
    }

    public Specialization Create(Specialization item)
    {
        var model = item.ToModel();

        _context.Specializations.Add(model);
        Save();

        return model.ToDomain();
    }

    public Specialization Update(Specialization item)
    {
        var model = item.ToModel();

        _context.Specializations.Update(model);
        Save();

        return model.ToDomain();
    }

    public Specialization Delete(int id)
    {
        var specialization = _context.Specializations.AsNoTracking().First(s => s.Id == id);

        _context.Specializations.Remove(specialization);
        Save();

        return specialization.ToDomain();
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}
