using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Xhznl.DataDictionary
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(DataDictionaryDomainSharedModule)
    )]
    public class DataDictionaryDomainModule : AbpModule
    {

    }
}
