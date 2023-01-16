using domain.Logic;
using domain.Logic.Repositories;
using domain.Models;

namespace domain.UseCases;

public class ScheduleService
{
    private IScheduleRepository _db;

    public ScheduleService(IScheduleRepository db)
    {
        _db = db;
    }

    public Result<Schedule> GetSchedule(int doctorId, DateOnly date)
    {
        var success = _db.GetSchedule(doctorId, date);

        if (success.IsFailure)
        {
            return Result.Fail<Schedule>("ScheduleService.GetSchedule: " + success.Error);
        }

        return success;
    }

    public Result<Schedule> CreateSchedule(Schedule schedule)
    {
        if (schedule.IsValid().IsFailure)
        {
            return Result.Fail<Schedule>(
                "ScheduleService.CreateSchedule: " + schedule.IsValid().Error
            );
        }

        if (_db.IsDateFree(schedule.DoctorId, schedule.Date).IsFailure)
        {
            return Result.Fail<Schedule>("ScheduleService.CreateSchedule: Date is busy.");
        }

        var success = _db.Create(schedule);

        if (success.IsFailure)
        {
            return Result.Fail<Schedule>("ScheduleService.CreateSchedule: " + success.Error);
        }

        return success;
    }

    public Result<Schedule> UpdateSchedule(Schedule schedule)
    {
        if (schedule.IsValid().IsFailure)
        {
            return Result.Fail<Schedule>(
                "ScheduleService.UpdateSchedule: " + schedule.IsValid().Error
            );
        }

        var success = _db.Update(schedule);

        if (success.IsFailure)
        {
            return Result.Fail<Schedule>("ScheduleService.UpdateSchedule: " + success.Error);
        }

        return success;
    }

    public Result DeleteSchedule(int id)
    {
        var success = _db.Delete(id);

        if (success.IsFailure)
        {
            return Result.Fail("ScheduleService.DeleteSchedule: " + success.Error);
        }

        return _db.Delete(id);
    }
}
