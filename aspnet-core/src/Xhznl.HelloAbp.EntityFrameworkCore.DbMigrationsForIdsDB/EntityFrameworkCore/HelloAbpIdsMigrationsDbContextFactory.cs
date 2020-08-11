using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Xhznl.HelloAbp.EntityFrameworkCore
{
    public class HelloAbpIdsMigrationsDbContextFactory : IDesignTimeDbContextFactory<HelloAbpIdsMigrationsDbContext>
    {
        public HelloAbpIdsMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<HelloAbpIdsMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("AbpIdentityServer"));

            return new HelloAbpIdsMigrationsDbContext(builder.Options);
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
