using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Xhznl.HelloAbp.HttpApi.Client.ConsoleTestApp
{
    [DependsOn(
        typeof(HelloAbpHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class HelloAbpConsoleApiClientModule : AbpModule
    {
        
    }
}
