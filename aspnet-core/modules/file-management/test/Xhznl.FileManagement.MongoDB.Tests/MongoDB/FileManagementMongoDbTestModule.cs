using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace Xhznl.FileManagement.MongoDB
{
    [DependsOn(
        typeof(FileManagementTestBaseModule),
        typeof(FileManagementMongoDbModule)
        )]
    public class FileManagementMongoDbTestModule : AbpModule
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