using Domain.Logic;
using Domain.Logic.Repositories;
using Domain.Models;

namespace Domain.UseCases;

public class ScheduleService
{
    private IScheduleRepository _db;

    public ScheduleService(IScheduleRepository db)
    {
        _db = db;
    }

    public Result<Schedule> GetScheduleByDate(int doctorId, DateOnly date)
    {
        if (doctorId == 0)
        {
            return Result.Fail<Schedule>("ScheduleService.GetSchedule: Invalid doctorId.");
        }

        try
        {
            var success = _db.GetByDate(doctorId, date);

            if (success is null)
            {
                return Result.Fail<Schedule>(
                    "ScheduleService.GetSchedule: Schedule doesn't exist."
                );
            }

            return Result.Ok<Schedule>(success);
        }
        catch (Exception ex)
        {
            return Result.Fail<Schedule>("ScheduleService.GetSchedule: " + ex.Message);
        }
    }

    public Result<Schedule> CreateSchedule(Schedule schedule)
    {
        if (schedule.IsValid().IsFailure)
        {
            return Result.Fail<Schedule>(
                "ScheduleService.CreateSchedule: " + schedule.IsValid().Error
            );
        }

        try
        {
            var item = _db.GetByDate(schedule.DoctorId, schedule.Date);

            if (item is not null)
            {
                return Result.Fail<Schedule>("ScheduleService.CreateSchedule: Date is busy.");
            }

            var success = _db.Create(schedule);

            return Result.Ok<Schedule>(success);
        }
        catch (Exception ex)
        {
            return Result.Fail<Schedule>("ScheduleService.CreateSchedule: " + ex.Message);
        }
    }

    public Result<Schedule> UpdateSchedule(Schedule schedule)
    {
        if (schedule.IsValid().IsFailure)
        {
            return Result.Fail<Schedule>(
                "ScheduleService.UpdateSchedule: " + schedule.IsValid().Error
            );
        }

        try
        {
            var success = _db.Update(schedule);

            return Result.Ok<Schedule>(success);
        }
        catch (Exception ex)
        {
            return Result.Fail<Schedule>("ScheduleService.UpdateSchedule: " + ex.Message);
        }
    }

    public Result<Schedule> DeleteSchedule(int id)
    {
        if (id == 0)
        {
            return Result.Fail<Schedule>("ScheduleService.DeleteSchedule: Invalid id.");
        }
        try
        {
            var success = _db.Delete(id);

            return Result.Ok<Schedule>(success);
        }
        catch (Exception ex)
        {
            return Result.Fail<Schedule>("ScheduleService.DeleteSchedule: " + ex.Message);
        }
    }
}
