using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xhznl.HelloAbp;
using Xunit;

namespace Volo.Abp.Identity
{
    public class UserAppService_Tests: HelloAbpApplicationTestBase
    {
        private readonly IHelloIdentityUserAppService _userAppService;
        public UserAppService_Tests()
        {
            _userAppService = ServiceProvider.GetRequiredService<IHelloIdentityUserAppService>();
        }
    }
}
