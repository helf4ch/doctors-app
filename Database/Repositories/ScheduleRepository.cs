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

    public async Task<Schedule?> Get(int id)
    {
        var result = await _context.Schedules.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);

        return result?.ToDomain();
    }

    public async Task<Schedule> Create(Schedule item)
    {
        var model = item.ToModel();

        await _context.Schedules.AddAsync(item.ToModel());
        await Save();

        return model.ToDomain();
    }

    public async Task<Schedule> Update(Schedule item)
    {
        var model = item.ToModel();

        _context.Schedules.Update(model);
        await Save();

        return model.ToDomain();
    }

    public async Task<Schedule> Delete(int id)
    {
        var schedule = await _context.Schedules.AsNoTracking().FirstAsync(s => s.Id == id);

        _context.Schedules.Remove(schedule);
        await Save();

        return schedule.ToDomain();
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<Schedule?> GetByDate(int doctorId, DateOnly date)
    {
        var result = await _context.Schedules
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.DoctorId == doctorId && s.Date == date);

        return result?.ToDomain();
    }
}
