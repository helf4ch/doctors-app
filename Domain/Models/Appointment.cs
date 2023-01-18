namespace Domain.Models;

public class Appointment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int DoctorId { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }

    public Appointment(int id, int userId, int doctorId, DateOnly date, TimeOnly startTime)
    {
        Id = id;
        UserId = userId;
        DoctorId = doctorId;
        Date = date;
        StartTime = startTime;
    }
}
