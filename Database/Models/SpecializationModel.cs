using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Database.Models;

[Index(nameof(Name), IsUnique = true)]
public class SpecializationModel
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    public List<DoctorModel> Doctors { get; set; }
}
