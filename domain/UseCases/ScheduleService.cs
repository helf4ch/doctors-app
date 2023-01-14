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
            return Result.Fail<Schedule>("ScheduleService: " + success.Error);
        }

        return success;
    }

    public Result<Schedule> CreateSchedule(Schedule schedule)
    {
        if (schedule.IsValid().IsFailure)
        {
            return Result.Fail<Schedule>("ScheduleService: " + schedule.IsValid().Error);
        }

        if (_db.IsExists(schedule.Id).Success)
        {
            return Result.Fail<Schedule>("ScheduleService: Schedule is already exists.");
        }

        if (_db.IsDateFree(schedule.DoctorId, schedule.Date).IsFailure)
        {
            return Result.Fail<Schedule>("ScheduleService: Date is busy.");
        }

        var success = _db.Create(schedule);

        if (success.IsFailure)
        {
            return Result.Fail<Schedule>("ScheduleService: " + success.Error);
        }

        return success;
    }

    public Result<Schedule> UpdateSchedule(Schedule schedule)
    {
        if (schedule.IsValid().IsFailure)
        {
            return Result.Fail<Schedule>("ScheduleService: " + schedule.IsValid().Error);
        }

        if (_db.IsExists(schedule.Id).IsFailure)
        {
            return Result.Fail<Schedule>("ScheduleService: Schedule doesn't exists.");
        }

        var success = _db.Update(schedule);

        if (success.IsFailure)
        {
            return Result.Fail<Schedule>("ScheduleService: " + success.Error);
        }

        return success;
    }

    public Result DeleteSchedule(int id)
    {
        if (_db.IsExists(id).IsFailure)
        {
            return Result.Fail("ScheduleService: Schedule doesn't exists.");
        }

        var success = _db.Delete(id);

        if (success.IsFailure)
        {
            return Result.Fail("ScheduleService: " + success.Error);
        }

        return _db.Delete(id);
    }
}
