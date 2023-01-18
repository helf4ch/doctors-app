using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Database.Models;

[Index(nameof(Name), IsUnique = true)]
public class RoleModel
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    public List<UserModel> Users { get; set; }
}
