using Xhznl.HelloAbp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Xhznl.HelloAbp
{
    [DependsOn(
        typeof(HelloAbpEntityFrameworkCoreTestModule)
        )]
    public class HelloAbpDomainTestModule : AbpModule
    {

    }
}