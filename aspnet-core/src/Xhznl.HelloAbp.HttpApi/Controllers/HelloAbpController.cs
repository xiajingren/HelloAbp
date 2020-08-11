using Xhznl.HelloAbp.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Xhznl.HelloAbp.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class HelloAbpController : AbpController
    {
        protected HelloAbpController()
        {
            LocalizationResource = typeof(HelloAbpResource);
        }
    }
}