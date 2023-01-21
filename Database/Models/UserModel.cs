using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

#nullable disable

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
    [MaxLength(32)]
    public string Password { get; set; }

    [Required]
    [MaxLength(8)]
    public string Salt { get; set; }

    public RoleModel Role { get; set; }
    public List<AppointmentModel> Appointments { get; set; }
}
