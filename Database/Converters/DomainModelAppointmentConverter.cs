using Database.Models;
using Domain.Models;

#nullable disable

namespace Database.Converters;

public static class DomainModelAppointmentConverter
{
    public static AppointmentModel ToModel(this Appointment appointment)
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

    public static Appointment ToDomain(this AppointmentModel appointment)
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
