using Database.Converters;
using Domain.Logic.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories;

public class ScheduleRepository : IScheduleRepository
{
    private readonly ApplicationContext _context;

    public ScheduleRepository(ApplicationContext context)
    {
        _context = context;
    }

    public Schedule? Get(int id)
    {
        var result = _context.Schedules.FirstOrDefault(s => s.Id == id)?.ToDomain();

        return result;
    }

    public void Create(Schedule item)
    {
        _context.Schedules.Add(item.ToModel());
        Save();
    }

    public void Update(Schedule item)
    {
        _context.Schedules.Update(item.ToModel());
        Save();
    }

    public void Delete(int id)
    {
        var schedule = _context.Schedules.AsNoTracking().First(s => s.Id == id);
        _context.Schedules.Remove(schedule);
        Save();
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public Schedule? Get(int doctorId, DateOnly date)
    {
        var result = _context.Schedules
            .FirstOrDefault(s => s.DoctorId == doctorId && s.Date == date)
            ?.ToDomain();

        return result;
    }
}
