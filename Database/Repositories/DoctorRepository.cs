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

    public Doctor? Get(int id)
    {
        var result = _context.Doctors.AsNoTracking().FirstOrDefault(d => d.Id == id)?.ToDomain();

        return result;
    }

    public Doctor Create(Doctor item)
    {
        var model = item.ToModel();

        _context.Doctors.Add(model);
        Save();

        return model.ToDomain();
    }

    public Doctor Update(Doctor item)
    {
        var model = item.ToModel();

        _context.Doctors.Update(model);
        Save();

        return model.ToDomain();
    }

    public Doctor Delete(int id)
    {
        var doctor = _context.Doctors.AsNoTracking().First(d => d.Id == id);

        _context.Doctors.Remove(doctor);
        Save();

        return doctor.ToDomain();
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public List<Doctor> GetAll()
    {
        var result = _context.Doctors.AsNoTracking().Select(d => d.ToDomain()).ToList();

        return result;
    }

    public List<Doctor> GetAllBySpecialization(int specializationId)
    {
        var result = _context.Doctors
            .AsNoTracking()
            .Where(d => d.SpecializationId == specializationId)
            .Select(d => d.ToDomain())
            .ToList();

        return result;
    }
}
