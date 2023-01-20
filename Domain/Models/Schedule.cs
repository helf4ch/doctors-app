using Domain.Logic;

namespace Domain.Models;

public class Schedule
{
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartOfShift { get; set; }
    public TimeOnly EndOfShift { get; set; }

    public Result IsValid()
    {
        if (StartOfShift >= EndOfShift)
        {
            return Result.Fail(
                "Schedule.IsValid: EndOfShift can't be less or equal then StartOfShift."
            );
        }

        return Result.Ok();
    }
}
