using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Xhznl.DataDictionary.MongoDB
{
    [ConnectionStringName(DataDictionaryDbProperties.ConnectionStringName)]
    public interface IDataDictionaryMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
