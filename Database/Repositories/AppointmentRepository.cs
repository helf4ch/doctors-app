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

    public async Task<Appointment?> Get(int id)
    {
        var result = await _context.Appointments
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);

        return result?.ToDomain();
    }

    public async Task<Appointment> Create(Appointment item)
    {
        var model = item.ToModel();

        await _context.Appointments.AddAsync(model);
        await Save();

        return model.ToDomain();
    }

    public async Task<Appointment> Update(Appointment item)
    {
        var model = item.ToModel();

        _context.Appointments.Update(model);
        await Save();

        return model.ToDomain();
    }

    public async Task<Appointment> Delete(int id)
    {
        var appointment = await _context.Appointments.AsNoTracking().FirstAsync(a => a.Id == id);

        _context.Appointments.Remove(appointment);
        await Save();

        return appointment.ToDomain();
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<List<Appointment>> GetAllByTime(
        int doctorId,
        DateOnly date,
        TimeOnly startTime,
        TimeOnly endTime
    )
    {
        var result = await _context.Appointments
            .AsNoTracking()
            .Where(
                a =>
                    a.DoctorId == doctorId
                    && a.Date == date
                    && a.StartTime >= startTime
                    && a.StartTime <= endTime
            )
            .Select(a => a.ToDomain())
            .ToListAsync();

        return result;
    }

    public async Task<List<Appointment>> GetAllBySpecialization(int specializationId, DateOnly date)
    {
        var result = await _context.Doctors
            .AsNoTracking()
            .Where(d => d.SpecializationId == specializationId)
            .Join(_context.Appointments, d => d.Id, a => a.DoctorId, (d, a) => a.ToDomain())
            .ToListAsync();

        return result;
    }
}
