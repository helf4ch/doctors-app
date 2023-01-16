using domain.Logic;
using domain.Models;

namespace domain.UseCases.Interfaces;

public interface IScheduleService
{
    Result<Schedule> GetSchedule(int doctorId, DateOnly date);
    Result<Schedule> CreateSchedule(Schedule schedule);
    Result<Schedule> UpdateSchedule(Schedule schedule);
    Result DeleteSchedule(int id);
}
