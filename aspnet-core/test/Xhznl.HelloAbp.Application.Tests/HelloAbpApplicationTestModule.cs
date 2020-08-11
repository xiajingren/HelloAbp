using Volo.Abp.Modularity;

namespace Xhznl.HelloAbp
{
    [DependsOn(
        typeof(HelloAbpApplicationModule),
        typeof(HelloAbpDomainTestModule)
        )]
    public class HelloAbpApplicationTestModule : AbpModule
    {

    }
}