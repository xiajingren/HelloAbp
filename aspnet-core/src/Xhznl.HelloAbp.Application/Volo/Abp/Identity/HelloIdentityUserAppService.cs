using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Volo.Abp.Identity
{
    [RemoteService(false)]
    public class HelloIdentityUserAppService : IdentityAppServiceBase, IHelloIdentityUserAppService
    {
        protected IdentityUserManager UserManager { get; }
        public HelloIdentityUserAppService(IdentityUserManager userManager)
        {
            UserManager = userManager;
        }

        [Authorize(HelloIdentityPermissions.Users.DistributionOrganizationUnit)]
        public virtual async Task AddToOrganizationUnitAsync(Guid userId, Guid ouId)
        {
            if (await UserManager.IsInOrganizationUnitAsync(userId, ouId))
            {
                // TODO:Replace the ID with the name later
                throw new UserFriendlyException(L["Identity.OrganizationUnit.MemberExists", userId, ouId]);
            }

            await UserManager.AddToOrganizationUnitAsync(userId, ouId);
        }
    }
}
