using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Xhznl.DataDictionary.MongoDB
{
    [ConnectionStringName(DataDictionaryDbProperties.ConnectionStringName)]
    public class DataDictionaryMongoDbContext : AbpMongoDbContext, IDataDictionaryMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureDataDictionary();
        }
    }
}