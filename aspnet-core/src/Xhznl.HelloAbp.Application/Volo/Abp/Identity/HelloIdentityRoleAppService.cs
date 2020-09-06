using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Xhznl.HelloAbp.Localization;

namespace Volo.Abp.Identity
{
    [RemoteService(IsEnabled = false)]
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IIdentityRoleAppService), 
        typeof(IdentityRoleAppService),
        typeof(IHelloIdentityRoleAppService),
        typeof(HelloIdentityRoleAppService))]
    public class HelloIdentityRoleAppService : IdentityRoleAppService, IHelloIdentityRoleAppService
    {
        private IStringLocalizer<HelloAbpResource> _localizer;
        protected OrganizationUnitManager OrgManager { get; }
        public HelloIdentityRoleAppService(IdentityRoleManager roleManager,
            IIdentityRoleRepository roleRepository,
            IStringLocalizer<HelloAbpResource> localizer,
            OrganizationUnitManager orgManager) : base(roleManager, roleRepository)
        {
            _localizer = localizer;
            OrgManager = orgManager;
        }

        [Authorize(HelloIdentityPermissions.Roles.AddOrganizationUnitRole)]
        public Task AddToOrganizationUnitAsync(Guid roleId, Guid ouId)
        {
            return OrgManager.AddRoleToOrganizationUnitAsync(roleId, ouId);
        }

        [Authorize(IdentityPermissions.Roles.Create)]
        [Authorize(HelloIdentityPermissions.Roles.AddOrganizationUnitRole)]
        public virtual async Task<IdentityRoleDto> CreateAsync(IdentityRoleOrgCreateDto input)
        {
            var role = await base.CreateAsync(
                ObjectMapper.Map<IdentityRoleOrgCreateDto, IdentityRoleCreateDto>(input)
            );
            if (input.OrgId.HasValue)
            {
                await OrgManager.AddRoleToOrganizationUnitAsync(role.Id,input.OrgId.Value);
            }
            return role;
        }
    }
}
