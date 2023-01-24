using Database.Converters;
using Domain.Logic.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories;

public class DoctorRepository : IDoctorRepository
{
    private readonly ApplicationContext _context;

    public DoctorRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Doctor?> Get(int id)
    {
        var result = await _context.Doctors.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);

        return result?.ToDomain();
    }

    public async Task<Doctor> Create(Doctor item)
    {
        var model = item.ToModel();

        await _context.Doctors.AddAsync(model);
        await Save();

        return model.ToDomain();
    }

    public async Task<Doctor> Update(Doctor item)
    {
        var model = item.ToModel();

        _context.Doctors.Update(model);
        await Save();

        return model.ToDomain();
    }

    public async Task<Doctor> Delete(int id)
    {
        var doctor = await _context.Doctors.AsNoTracking().FirstAsync(d => d.Id == id);

        _context.Doctors.Remove(doctor);
        await Save();

        return doctor.ToDomain();
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<List<Doctor>> GetAll()
    {
        var result = await _context.Doctors.AsNoTracking().Select(d => d.ToDomain()).ToListAsync();

        return result;
    }

    public async Task<List<Doctor>> GetAllBySpecialization(int specializationId)
    {
        var result = await _context.Doctors
            .AsNoTracking()
            .Where(d => d.SpecializationId == specializationId)
            .Select(d => d.ToDomain())
            .ToListAsync();

        return result;
    }
}
