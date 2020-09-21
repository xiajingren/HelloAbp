using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using Xhznl.DataDictionary.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace Xhznl.DataDictionary
{
    [DependsOn(
        typeof(AbpValidationModule)
    )]
    public class DataDictionaryDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<DataDictionaryDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<DataDictionaryResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/DataDictionary");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("DataDictionary", typeof(DataDictionaryResource));
            });
        }
    }
}
