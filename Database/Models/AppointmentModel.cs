using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Database.Models;

public class AppointmentModel
{
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public int DoctorId { get; set; }

    [Required]
    public DateOnly Date { get; set; }

    [Required]
    public TimeOnly StartTime { get; set; }

    public UserModel User { get; set; }
    public DoctorModel Doctor { get; set; }
}
