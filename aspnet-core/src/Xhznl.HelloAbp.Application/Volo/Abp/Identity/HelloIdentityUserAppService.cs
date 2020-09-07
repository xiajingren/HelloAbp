using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Volo.Abp.DependencyInjection;
using Xhznl.HelloAbp.Features;
using Xhznl.HelloAbp.Localization;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace Volo.Abp.Identity
{
    [RemoteService(IsEnabled = false)]
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IIdentityUserAppService),
        typeof(IdentityUserAppService),
        typeof(IHelloIdentityUserAppService),
        typeof(HelloIdentityUserAppService))]
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

        [Authorize(IdentityPermissions.Users.Create)]
        [Authorize(HelloIdentityPermissions.Users.DistributionOrganizationUnit)]
        public virtual async Task<IdentityUserDto> CreateAsync(IdentityUserOrgCreateDto input)
        {
            var identity = await CreateAsync(
                ObjectMapper.Map<IdentityUserOrgCreateDto, IdentityUserCreateDto>(input)
            );
            if (input.OrgIds != null)
            {
                await UserManager.SetOrganizationUnitsAsync(identity.Id, input.OrgIds.ToArray());
            }
            return identity;
        }

        [Authorize(HelloIdentityPermissions.Users.DistributionOrganizationUnit)]
        public virtual async Task AddToOrganizationUnitsAsync(Guid userId, List<Guid> ouIds)
        {
            await UserManager.SetOrganizationUnitsAsync(userId, ouIds.ToArray());
        }

        public virtual async Task<ListResultDto<OrganizationUnitDto>> GetListOrganizationUnitsAsync(Guid id, bool includeDetails = false)
        {
            var list = await UserRepository.GetOrganizationUnitsAsync(id, includeDetails);
            return new ListResultDto<OrganizationUnitDto>(
                ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(list)
            );
        }

        [Authorize(IdentityPermissions.Users.Update)]
        [Authorize(HelloIdentityPermissions.Users.DistributionOrganizationUnit)]
        public virtual async Task<IdentityUserDto> UpdateAsync(Guid id, IdentityUserOrgUpdateDto input)
        {
            var update = ObjectMapper.Map<IdentityUserOrgUpdateDto, IdentityUserUpdateDto>(input);
            var result = await base.UpdateAsync(id, update);
            await UserManager.SetOrganizationUnitsAsync(result.Id, input.OrgIds.ToArray());
            return result;
        }
    }
}
