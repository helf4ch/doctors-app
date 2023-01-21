using Domain.Models;

namespace Domain.Logic.Repositories;

public interface IScheduleRepository : IRepository<Schedule>
{
    Schedule? Get(int doctorId, DateOnly date);
}
