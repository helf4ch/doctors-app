using Database.Models;
using Domain.Models;

namespace Database.Converters;

public class DomainModelAppointmentConverter
{
    public static AppointmentModel ToModel(Appointment appointment)
    {
        return new AppointmentModel
        {
            Id = appointment.Id,
            UserId = appointment.UserId,
            DoctorId = appointment.DoctorId,
            Date = appointment.Date,
            StartTime = appointment.StartTime
        };
    }

    public static Appointment ToDomain(AppointmentModel appointment)
    {
        return new Appointment
        {
            Id = appointment.Id,
            UserId = appointment.UserId,
            DoctorId = appointment.DoctorId,
            Date = appointment.Date,
            StartTime = appointment.StartTime
        };
    }
}
