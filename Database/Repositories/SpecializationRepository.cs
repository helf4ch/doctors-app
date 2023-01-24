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

    public async Task<Specialization?> Get(int id)
    {
        var result = await _context.Specializations
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);

        return result?.ToDomain();
    }

    public async Task<Specialization> Create(Specialization item)
    {
        var model = item.ToModel();

        await _context.Specializations.AddAsync(model);
        await Save();

        return model.ToDomain();
    }

    public async Task<Specialization> Update(Specialization item)
    {
        var model = item.ToModel();

        _context.Specializations.Update(model);
        await Save();

        return model.ToDomain();
    }

    public async Task<Specialization> Delete(int id)
    {
        var specialization = await _context.Specializations
            .AsNoTracking()
            .FirstAsync(s => s.Id == id);

        _context.Specializations.Remove(specialization);
        await Save();

        return specialization.ToDomain();
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}
