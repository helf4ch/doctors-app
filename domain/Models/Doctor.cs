using domain.Logic;

namespace domain.Models;

public class Doctor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Secondname { get; set; }
    public string Surname { get; set; }
    public Specialization Specialization { get; set; }
    public int AppointmentTimeMinutes { get; set; }

    public Doctor(
        int id,
        string name,
        string secondname,
        string surname,
        Specialization specialization,
        int appointmentTimeMinutes
    )
    {
        Id = id;
        Name = name;
        Secondname = secondname;
        Surname = surname;
        Specialization = specialization;
        AppointmentTimeMinutes = appointmentTimeMinutes;
    }

    public Result IsValid()
    {
        if (string.IsNullOrEmpty(Name))
        {
            return Result.Fail("Doctor.IsValid: Null or empty Name.");
        }

        if (string.IsNullOrEmpty(Secondname))
        {
            return Result.Fail("Doctor.IsValid: Null or empty Secondname.");
        }

        if (string.IsNullOrEmpty(Surname))
        {
            return Result.Fail("Doctor.IsValid: Null or empty Surname.");
        }

        if (AppointmentTimeMinutes == 0)
        {
            return Result.Fail("Doctor.IsValid: AppointmentTimeMinutes can't be 0.");
        }

        if (Specialization.IsValid().IsFailure)
        {
            return Result.Fail("Doctor.IsValid: " + Specialization.IsValid().Error);
        }

        return Result.Ok();
    }
}
