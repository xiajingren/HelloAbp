using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Xhznl.HelloAbp.EntityFrameworkCore
{
    [DependsOn(
        typeof(HelloAbpEntityFrameworkCoreModule)
        )]
    public class HelloAbpEntityFrameworkCoreDbMigrationsIdsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<HelloAbpIdsMigrationsDbContext>();
        }
    }
}
