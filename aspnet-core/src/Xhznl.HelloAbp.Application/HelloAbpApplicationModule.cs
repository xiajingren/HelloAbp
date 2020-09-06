using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.ObjectExtending.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;

namespace Xhznl.HelloAbp
{
    [DependsOn(
        typeof(HelloAbpDomainModule),
        typeof(AbpAccountApplicationModule),
        typeof(HelloAbpApplicationContractsModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpFeatureManagementApplicationModule),
        typeof(EasyAbp.Abp.SettingUi.SettingUiApplicationModule)
        )]
    public class HelloAbpApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {           
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<HelloAbpApplicationModule>();
            });
        }
    }
}
