using Domain.Logic;

namespace Domain.Models;

public class Schedule
{
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartOfShift { get; set; }
    public TimeOnly EndOfShift { get; set; }

    public Schedule(int id, int doctorId, DateOnly date, TimeOnly startOfShift, TimeOnly endOfShift)
    {
        Id = id;
        DoctorId = doctorId;
        Date = date;
        StartOfShift = startOfShift;
        EndOfShift = endOfShift;
    }

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
