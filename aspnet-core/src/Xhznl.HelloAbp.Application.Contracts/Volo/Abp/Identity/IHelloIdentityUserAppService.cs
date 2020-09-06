using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Volo.Abp.Identity
{
    public interface IHelloIdentityUserAppService : IApplicationService
    {
        Task AddToOrganizationUnitAsync(Guid userId, Guid ouId);

        Task<IdentityUserDto> CreateAsync(IdentityUserOrgCreateDto input);
    }
}
