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
        var result = _context.Schedules.AsNoTracking().FirstOrDefault(s => s.Id == id)?.ToDomain();

        return result;
    }

    public Schedule Create(Schedule item)
    {
        var model = item.ToModel();

        _context.Schedules.Add(item.ToModel());
        Save();

        return model.ToDomain();
    }

    public Schedule Update(Schedule item)
    {
        var model = item.ToModel();

        _context.Schedules.Update(model);
        Save();

        return model.ToDomain();
    }

    public Schedule Delete(int id)
    {
        var schedule = _context.Schedules.AsNoTracking().First(s => s.Id == id);

        _context.Schedules.Remove(schedule);
        Save();

        return schedule.ToDomain();
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public Schedule? Get(int doctorId, DateOnly date)
    {
        var result = _context.Schedules
            .AsNoTracking()
            .FirstOrDefault(s => s.DoctorId == doctorId && s.Date == date)
            ?.ToDomain();

        return result;
    }
}
