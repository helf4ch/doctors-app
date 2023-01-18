using System.ComponentModel.DataAnnotations;

namespace Database.Models;

public class DoctorModel
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [MaxLength(50)]
    public string Secondname { get; set; }

    [MaxLength(50)]
    public string Surname { get; set; }

    [Required]
    public int SpecializationId { get; set; }

    [Required]
    public int AppointmentTimeMinutes { get; set; }

    public SpecializationModel Specialization { get; set; }
    public List<AppointmentModel> Appointments { get; set; }
    public List<ScheduleModel> Schedules { get; set; }
}
