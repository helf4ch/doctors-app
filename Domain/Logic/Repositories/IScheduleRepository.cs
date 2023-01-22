using Domain.Models;

namespace Domain.Logic.Repositories;

public interface IScheduleRepository : IRepository<Schedule>
{
    Schedule? GetByDate(int doctorId, DateOnly date);
}
