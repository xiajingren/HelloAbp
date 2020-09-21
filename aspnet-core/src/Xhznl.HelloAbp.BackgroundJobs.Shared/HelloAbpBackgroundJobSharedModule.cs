using Volo.Abp.Emailing;
using Volo.Abp.Modularity;

namespace Xhznl.HelloAbp
{
    [DependsOn(typeof(HelloAbpDomainModule),
        typeof(AbpEmailingModule))]
    public class HelloAbpBackgroundJobSharedModule : AbpModule
    {
        
    }
}