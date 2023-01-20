using Database.Models;
using Domain.Models;

namespace Database.Converters;

public static class DomainModelScheduleConverter
{
    public static ScheduleModel ToModel(this Schedule schedule)
    {
        return new ScheduleModel
        {
            Id = schedule.Id,
            DoctorId = schedule.DoctorId,
            Date = schedule.Date,
            StartOfShift = schedule.StartOfShift,
            EndOfShift = schedule.EndOfShift
        };
    }

    public static Schedule ToDomain(this ScheduleModel schedule)
    {
        return new Schedule
        {
            Id = schedule.Id,
            DoctorId = schedule.DoctorId,
            Date = schedule.Date,
            StartOfShift = schedule.StartOfShift,
            EndOfShift = schedule.EndOfShift
        };
    }
}
