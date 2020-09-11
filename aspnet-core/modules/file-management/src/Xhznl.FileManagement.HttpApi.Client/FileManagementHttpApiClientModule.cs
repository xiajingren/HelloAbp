using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace Xhznl.FileManagement
{
    [DependsOn(
        typeof(FileManagementApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class FileManagementHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "FileManagement";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(FileManagementApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
