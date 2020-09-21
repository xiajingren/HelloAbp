using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Xhznl.DataDictionary.EntityFrameworkCore
{
    public class DataDictionaryHttpApiHostMigrationsDbContext : AbpDbContext<DataDictionaryHttpApiHostMigrationsDbContext>
    {
        public DataDictionaryHttpApiHostMigrationsDbContext(DbContextOptions<DataDictionaryHttpApiHostMigrationsDbContext> options)
            : base(options)
        {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureDataDictionary();
        }
    }
}
