using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Xhznl.DataDictionary.EntityFrameworkCore
{
    [ConnectionStringName(DataDictionaryDbProperties.ConnectionStringName)]
    public interface IDataDictionaryDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
        DbSet<DataDictionary> DataDictionaries { get; }

        DbSet<DataDictionaryDetail> DataDictionaryDetails { get; }
    }
}