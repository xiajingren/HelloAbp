using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Xhznl.DataDictionary
{
    [DependsOn(
        typeof(DataDictionaryHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class DataDictionaryConsoleApiClientModule : AbpModule
    {
        
    }
}
