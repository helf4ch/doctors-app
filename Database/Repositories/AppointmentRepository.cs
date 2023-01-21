using Database.Converters;
using Domain.Logic.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly ApplicationContext _context;

    public AppointmentRepository(ApplicationContext context)
    {
        _context = context;
    }

    public Appointment? Get(int id)
    {
        var result = _context.Appointments.FirstOrDefault(a => a.Id == id)?.ToDomain();

        return result;
    }

    public void Create(Appointment item)
    {
        _context.Appointments.Add(item.ToModel());
        Save();
    }

    public void Update(Appointment item)
    {
        _context.Appointments.Update(item.ToModel());
        Save();
    }

    public void Delete(int id)
    {
        var appointment = _context.Appointments.AsNoTracking().First(a => a.Id == id);
        _context.Appointments.Remove(appointment);
        Save();
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public List<Appointment> GetAll(int doctorId, DateOnly date, TimeOnly time)
    {
        var result = _context.Appointments
            .Where(a => a.DoctorId == doctorId && a.Date == date && a.StartTime == time)
            .Select(a => a.ToDomain())
            .ToList();

        return result;
    }

    public List<Appointment> GetAll(int specializationId, DateOnly date)
    {
        var result = _context.Doctors
            .Where(d => d.SpecializationId == specializationId)
            .Join(_context.Appointments, d => d.Id, a => a.DoctorId, (d, a) => a.ToDomain())
            .ToList();

        return result;
    }
}
