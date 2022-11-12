namespace domain;

public class Appointment
{
    public uint UserId { get; set; }
    public uint DoctorId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
