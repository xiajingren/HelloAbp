using Localization.Resources.AbpUi;
using Xhznl.DataDictionary.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Xhznl.DataDictionary
{
    [DependsOn(
        typeof(DataDictionaryApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class DataDictionaryHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(DataDictionaryHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<DataDictionaryResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
