using Volo.Abp.Application.Services;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer.Localization;

namespace Volo.Abp.IdentityServer
{
    public abstract class IdentityServerAppServiceBase : ApplicationService
    {
        protected IdentityServerAppServiceBase()
        {
            this.ObjectMapperContext = typeof (AbpIdentityApplicationModule);
            this.LocalizationResource = typeof (AbpIdentityServerResource);
        }
    }
}