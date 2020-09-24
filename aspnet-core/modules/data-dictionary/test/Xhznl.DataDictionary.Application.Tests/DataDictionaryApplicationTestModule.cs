using Volo.Abp.Modularity;

namespace Xhznl.DataDictionary
{
    [DependsOn(
        typeof(DataDictionaryApplicationModule),
        typeof(DataDictionaryDomainTestModule)
        )]
    public class DataDictionaryApplicationTestModule : AbpModule
    {

    }
}
