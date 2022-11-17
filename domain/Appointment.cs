namespace domain;

public class Appointment
{
    public int UserId { get; set; }
    public int DoctorId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
