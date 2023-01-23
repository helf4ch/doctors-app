using Domain.Logic;

namespace Domain.Models;

public class Appointment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int DoctorId { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }

    public Result IsValid()
    {
        if (UserId == 0)
        {
            return Result.Fail("Appointment.IsValid: UserId is invalid.");
        }

        if (DoctorId == 0)
        {
            return Result.Fail("Appointment.IsValid: DoctorId is invalid.");
        }

        if (Date <= DateOnly.Parse(DateTime.UtcNow.ToString("d")))
        {
            return Result.Fail("Appointment.IsValid: Date is invalid.");
        }

        return Result.Ok();
    }
}
