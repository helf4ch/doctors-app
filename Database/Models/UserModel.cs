using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Database.Models;

[Index(nameof(PhoneNumber), IsUnique = true)]
public class UserModel
{
    public int Id { get; set; }

    [Required]
    [MaxLength(15)]
    public string PhoneNumber { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [MaxLength(50)]
    public string Secondname { get; set; }

    [MaxLength(50)]
    public string Surname { get; set; }

    [Required]
    public int RoleId { get; set; }

    [Required]
    public string Password { get; set; }

    public RoleModel Role { get; set; }
    public List<AppointmentModel> Appointments { get; set; }
}
