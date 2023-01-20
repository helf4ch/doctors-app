using Database.Models;
using Domain.Models;

namespace Database.Converters;

public class DomainModelScheduleConverter
{
    public static ScheduleModel ToModel(Schedule schedule)
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

    public static Schedule ToDomain(ScheduleModel schedule)
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
