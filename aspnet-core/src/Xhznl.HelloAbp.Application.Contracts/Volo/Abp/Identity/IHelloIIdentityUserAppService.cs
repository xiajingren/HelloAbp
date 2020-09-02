using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Volo.Abp.Identity
{
    public interface IHelloIIdentityUserAppService : IApplicationService
    {
        Task AddToOrganizationUnitAsync(Guid userId, Guid ouId);
    }
}
