using System;
using System.Collections.Generic;
using System.Text;
using Xhznl.HelloAbp.Localization;
using Volo.Abp.Application.Services;

namespace Xhznl.HelloAbp
{
    /* Inherit your application services from this class.
     */
    public abstract class HelloAbpAppService : ApplicationService
    {
        protected HelloAbpAppService()
        {
            LocalizationResource = typeof(HelloAbpResource);
        }
    }
}
