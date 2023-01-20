using Database.Models;
using Domain.Models;

namespace Database.Converters;

public static class DomainModelDoctorConverter
{
    public static DoctorModel ToModel(this Doctor doctor)
    {
        return new DoctorModel
        {
            Id = doctor.Id,
            Name = doctor.Name,
            Secondname = doctor.Secondname,
            Surname = doctor.Surname,
            SpecializationId = doctor.SpecializationId,
            AppointmentTimeMinutes = doctor.AppointmentTimeMinutes
        };
    }

    public static Doctor ToDomain(this DoctorModel doctor)
    {
        return new Doctor
        {
            Id = doctor.Id,
            Name = doctor.Name,
            Secondname = doctor.Secondname,
            Surname = doctor.Surname,
            SpecializationId = doctor.SpecializationId,
            AppointmentTimeMinutes = doctor.AppointmentTimeMinutes
        };
    }
}
