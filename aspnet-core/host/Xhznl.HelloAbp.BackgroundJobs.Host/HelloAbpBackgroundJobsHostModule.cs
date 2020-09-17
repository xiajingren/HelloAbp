using Volo.Abp;
using Volo.Abp.AspNetCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Xhznl.HelloAbp.Jobs;

namespace Xhznl.HelloAbp
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpAspNetCoreModule),
        typeof(HelloAbpApplicationModule))]
    public class HelloAbpBackgroundJobsHostModule : AbpModule
    {
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            //context.GetApplicationBuilder().UseConfiguredEndpoints(builder => builder.MapHangfireDashboard());
        }
    }
}
