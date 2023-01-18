using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Database;

public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
{
    public ApplicationContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();

        string connectionString =
            "server=localhost;user=doctors_app_server;password=help_me4815162342;database=doctors_app";

        optionsBuilder.UseMySql(connectionString, new MariaDbServerVersion(new Version(10, 9, 4)));

        return new ApplicationContext(optionsBuilder.Options);
    }
}
