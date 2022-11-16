namespace domain.Models;

public class Schedule
{
    public uint DoctorId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
