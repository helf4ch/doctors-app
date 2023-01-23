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

        if (Name.Length > 50)
        {
            return Result.Fail("Doctor.IsValid: Name has MaxLenght of 50.");
        }

        if (Secondname?.Length > 50)
        {
            return Result.Fail("Doctor.IsValid: Secondname has MaxLenght of 50.");
        }

        if (Surname?.Length > 50)
        {
            return Result.Fail("Doctor.IsValid: Surname has MaxLenght of 50.");
        }

        if (SpecializationId == 0)
        {
            return Result.Fail("Doctor.IsValid: SpecializationId is invalid.");
        }

        if (AppointmentTimeMinutes == 0)
        {
            return Result.Fail("Doctor.IsValid: AppointmentTimeMinutes can't be 0.");
        }

        return Result.Ok();
    }
}
