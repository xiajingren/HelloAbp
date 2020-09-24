using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.ObjectExtending.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;
using Xhznl.DataDictionary;
using Xhznl.FileManagement;

namespace Xhznl.HelloAbp
{
    [DependsOn(
        typeof(HelloAbpDomainSharedModule),
        typeof(AbpAccountApplicationContractsModule),
        typeof(AbpFeatureManagementApplicationContractsModule),
        typeof(AbpIdentityApplicationContractsModule),
        typeof(AbpPermissionManagementApplicationContractsModule),
        typeof(AbpTenantManagementApplicationContractsModule),
        typeof(AbpObjectExtendingModule),
        typeof(FileManagementApplicationContractsModule),
        typeof(DataDictionaryApplicationContractsModule)
    )]
    public class HelloAbpApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            HelloAbpDtoExtensions.Configure();

            ModuleExtensionConfigurationHelper
                .ApplyEntityConfigurationToApi(
                    IdentityModuleExtensionConsts.ModuleName,
                    IdentityModuleExtensionConsts.EntityNames.OrganizationUnit,
                    getApiTypes: new[] { typeof(OrganizationUnitDto) },
                    createApiTypes: new[] { typeof(OrganizationUnitCreateDto) },
                    updateApiTypes: new[] { typeof(OrganizationUnitUpdateDto) }
                );
        }
    }
}
