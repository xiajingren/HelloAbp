using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Xhznl.DataDictionary.EntityFrameworkCore
{
    public class DataDictionaryHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<DataDictionaryHttpApiHostMigrationsDbContext>
    {
        public DataDictionaryHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<DataDictionaryHttpApiHostMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("DataDictionary"));

            return new DataDictionaryHttpApiHostMigrationsDbContext(builder.Options);
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
