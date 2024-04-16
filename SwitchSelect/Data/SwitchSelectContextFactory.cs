using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SwitchSelect.Data;

public class SwitchSelectContextFactory : IDesignTimeDbContextFactory<SwitchSelectContext>
{
    public SwitchSelectContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("SwitchSelectConnection");

        var builder = new DbContextOptionsBuilder<SwitchSelectContext>();
        builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

        return new SwitchSelectContext(builder.Options);
    }
}
