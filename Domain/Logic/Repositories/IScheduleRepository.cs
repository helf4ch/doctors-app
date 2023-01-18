using domain.Models;

namespace domain.Logic.Repositories;

public interface IScheduleRepository : IRepository<Schedule>
{
    Result<Schedule> GetSchedule(int doctorId, DateOnly date);
    Result IsDateFree(int doctorId, DateOnly date);
}
