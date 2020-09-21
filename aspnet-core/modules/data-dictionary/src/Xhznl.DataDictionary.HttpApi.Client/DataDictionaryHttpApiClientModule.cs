using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace Xhznl.DataDictionary
{
    [DependsOn(
        typeof(DataDictionaryApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class DataDictionaryHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "DataDictionary";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(DataDictionaryApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
