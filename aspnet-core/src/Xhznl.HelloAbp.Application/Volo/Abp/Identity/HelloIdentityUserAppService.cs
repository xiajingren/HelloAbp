using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Volo.Abp.DependencyInjection;
using Xhznl.HelloAbp.Features;
using Xhznl.HelloAbp.Localization;

namespace Volo.Abp.Identity
{
    [RemoteService(IsEnabled = false)]
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IIdentityUserAppService), typeof(IdentityUserAppService))]
    public class HelloIdentityUserAppService : IdentityUserAppService, IHelloIdentityUserAppService
    {
        private readonly IStringLocalizer<HelloAbpResource> _localizer;

        public HelloIdentityUserAppService(IdentityUserManager userManager,
            IIdentityUserRepository userRepository,
            IIdentityRoleRepository roleRepository,
            IStringLocalizer<HelloAbpResource> localizer) : base(userManager, userRepository, roleRepository)
        {
            _localizer = localizer;
        }

        public override async Task<IdentityUserDto> CreateAsync(IdentityUserCreateDto input)
        {
            var userCount = (await FeatureChecker.GetOrNullAsync(HelloAbpFeatures.UserCount)).To<int>();
            var currentUserCount = await UserRepository.GetCountAsync();
            if (currentUserCount >= userCount)
            {
                throw new UserFriendlyException(_localizer["Feature:UserCount.Maximum", userCount]);
            }

            return await base.CreateAsync(input);
        }

        [Authorize(HelloIdentityPermissions.Users.DistributionOrganizationUnit)]
        public virtual async Task AddToOrganizationUnitAsync(Guid userId, Guid ouId)
        {
            if (await UserManager.IsInOrganizationUnitAsync(userId, ouId))
            {
                // TODO:Replace the ID with the name later
                throw new UserFriendlyException(_localizer["Identity.OrganizationUnit.MemberExists", userId, ouId]);
            }

            await UserManager.AddToOrganizationUnitAsync(userId, ouId);
        }


    }
}
