using Domain.Models;

namespace Domain.Logic.Repositories;

public interface IScheduleRepository : IRepository<Schedule>
{
    Result<Schedule> GetSchedule(int doctorId, DateOnly date);
    Result IsDateFree(int doctorId, DateOnly date);
}
