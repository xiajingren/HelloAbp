using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Xhznl.HelloAbp.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class HelloAbpMigrationsDbContextFactory : IDesignTimeDbContextFactory<HelloAbpMigrationsDbContext>
    {
        public HelloAbpMigrationsDbContext CreateDbContext(string[] args)
        {
            HelloAbpEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<HelloAbpMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new HelloAbpMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
