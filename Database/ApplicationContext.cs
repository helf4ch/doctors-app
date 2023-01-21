using Database.Models;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Database;

public class ApplicationContext : DbContext
{
    public DbSet<AppointmentModel> Appointments { get; set; }
    public DbSet<DoctorModel> Doctors { get; set; }
    public DbSet<RoleModel> Roles { get; set; }
    public DbSet<ScheduleModel> Schedules { get; set; }
    public DbSet<SpecializationModel> Specializations { get; set; }
    public DbSet<UserModel> Users { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}
