using System.ComponentModel.DataAnnotations;

namespace Database.Models;

public class ScheduleModel
{
    public int Id { get; set; }
    public int DoctorId { get; set; }

    [Required]
    public DateOnly Date { get; set; }

    [Required]
    public TimeOnly StartOfShift { get; set; }

    [Required]
    public TimeOnly EndOfShift { get; set; }

    public DoctorModel Doctor { get; set; }
}
