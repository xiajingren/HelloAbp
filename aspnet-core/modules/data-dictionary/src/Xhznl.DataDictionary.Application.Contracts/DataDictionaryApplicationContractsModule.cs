using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Xhznl.DataDictionary
{
    [DependsOn(
        typeof(DataDictionaryDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class DataDictionaryApplicationContractsModule : AbpModule
    {

    }
}
