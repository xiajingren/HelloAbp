using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;
using Xhznl.DataDictionary;
using Xhznl.FileManagement;

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
        typeof(EasyAbp.Abp.SettingUi.SettingUiApplicationModule),
        typeof(FileManagementApplicationModule),
        typeof(DataDictionaryApplicationModule)
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
