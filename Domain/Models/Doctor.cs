using Domain.Logic;

namespace Domain.Models;

public class Doctor
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Secondname { get; set; }
    public string? Surname { get; set; }
    public int SpecializationId { get; set; }
    public int AppointmentTimeMinutes { get; set; }

    public Result IsValid()
    {
        if (string.IsNullOrEmpty(Name))
        {
            return Result.Fail("Doctor.IsValid: Null or empty Name.");
        }

        if (AppointmentTimeMinutes == 0)
        {
            return Result.Fail("Doctor.IsValid: AppointmentTimeMinutes can't be 0.");
        }

        return Result.Ok();
    }
}
