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
        var result = _context.Doctors.FirstOrDefault(d => d.Id == id)?.ToDomain();

        return result;
    }

    public void Create(Doctor item)
    {
        _context.Doctors.Add(item.ToModel());
        Save();
    }

    public void Update(Doctor item)
    {
        _context.Doctors.Update(item.ToModel());
        Save();
    }

    public void Delete(int id)
    {
        var doctor = _context.Doctors.AsNoTracking().First(d => d.Id == id);
        _context.Doctors.Remove(doctor);
        Save();
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public List<Doctor> GetAll()
    {
        var result = _context.Doctors.Select(d => d.ToDomain()).ToList();

        return result;
    }

    public List<Doctor> GetAll(int specializationId)
    {
        var result = _context.Doctors
            .Where(d => d.SpecializationId == specializationId)
            .Select(d => d.ToDomain())
            .ToList();

        return result;
    }
}
