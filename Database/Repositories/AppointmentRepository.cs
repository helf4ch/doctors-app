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
        var result = _context.Appointments
            .AsNoTracking()
            .FirstOrDefault(a => a.Id == id)
            ?.ToDomain();

        return result;
    }

    public Appointment Create(Appointment item)
    {
        var model = item.ToModel();

        _context.Appointments.Add(model);
        Save();

        return model.ToDomain();
    }

    public Appointment Update(Appointment item)
    {
        var model = item.ToModel();

        _context.Appointments.Update(model);
        Save();

        return model.ToDomain();
    }

    public Appointment Delete(int id)
    {
        var appointment = _context.Appointments.AsNoTracking().First(a => a.Id == id);

        _context.Appointments.Remove(appointment);
        Save();

        return appointment.ToDomain();
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public List<Appointment> GetAllByTime(
        int doctorId,
        DateOnly date,
        TimeOnly startTime,
        TimeOnly endTime
    )
    {
        var result = _context.Appointments
            .AsNoTracking()
            .Where(
                a =>
                    a.DoctorId == doctorId
                    && a.Date == date
                    && a.StartTime >= startTime
                    && a.StartTime <= endTime
            )
            .Select(a => a.ToDomain())
            .ToList();

        return result;
    }

    public List<Appointment> GetAllBySpecialization(int specializationId, DateOnly date)
    {
        var result = _context.Doctors
            .AsNoTracking()
            .Where(d => d.SpecializationId == specializationId)
            .Join(_context.Appointments, d => d.Id, a => a.DoctorId, (d, a) => a.ToDomain())
            .ToList();

        return result;
    }
}
