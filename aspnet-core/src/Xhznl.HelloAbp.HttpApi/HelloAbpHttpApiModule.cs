using Localization.Resources.AbpUi;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.TenantManagement;
using Xhznl.DataDictionary;
using Xhznl.FileManagement;
using Xhznl.HelloAbp.Localization;

namespace Xhznl.HelloAbp
{
    [DependsOn(
        typeof(HelloAbpApplicationContractsModule),
        typeof(AbpAccountHttpApiModule),
        typeof(AbpIdentityHttpApiModule),
        typeof(AbpPermissionManagementHttpApiModule),
        typeof(AbpTenantManagementHttpApiModule),
        typeof(AbpFeatureManagementHttpApiModule),
        typeof(EasyAbp.Abp.SettingUi.SettingUiHttpApiModule),
        typeof(FileManagementHttpApiModule),
        typeof(DataDictionaryHttpApiModule)
        )]
    public class HelloAbpHttpApiModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<HelloAbpResource>()
                    .AddBaseTypes(
                        typeof(AbpUiResource)
                    );
            });
        }
    }
}
