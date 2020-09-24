using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace Xhznl.DataDictionary.MongoDB
{
    [DependsOn(
        typeof(DataDictionaryTestBaseModule),
        typeof(DataDictionaryMongoDbModule)
        )]
    public class DataDictionaryMongoDbTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var connectionString = MongoDbFixture.ConnectionString.EnsureEndsWith('/') +
                                   "Db_" +
                                    Guid.NewGuid().ToString("N");

            Configure<AbpDbConnectionOptions>(options =>
            {
                options.ConnectionStrings.Default = connectionString;
            });
        }
    }
}