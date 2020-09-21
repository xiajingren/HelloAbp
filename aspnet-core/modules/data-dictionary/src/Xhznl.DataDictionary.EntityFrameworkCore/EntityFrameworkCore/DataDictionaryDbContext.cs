using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Xhznl.DataDictionary.EntityFrameworkCore
{
    [ConnectionStringName(DataDictionaryDbProperties.ConnectionStringName)]
    public class DataDictionaryDbContext : AbpDbContext<DataDictionaryDbContext>, IDataDictionaryDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */

        public DataDictionaryDbContext(DbContextOptions<DataDictionaryDbContext> options) 
            : base(options)
        {

        }

        public DbSet<DataDictionary> DataDictionaries => throw new System.NotImplementedException();

        public DbSet<DataDictionaryDetail> DataDictionaryDetails => throw new System.NotImplementedException();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureDataDictionary();
        }
    }
}