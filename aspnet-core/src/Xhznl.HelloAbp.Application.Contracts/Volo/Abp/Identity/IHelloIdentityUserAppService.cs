using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Volo.Abp.Identity
{
    public interface IHelloIdentityUserAppService : IApplicationService
    {
        Task AddToOrganizationUnitsAsync(Guid userId, List<Guid> ouId);


        /// <summary>
        /// get list OrganizationUnits
        /// </summary>
        /// <param name="id">user id</param>
        /// <param name="includeDetails"></param>
        /// <returns></returns>
        Task<ListResultDto<OrganizationUnitDto>> GetListOrganizationUnitsAsync(Guid id, bool includeDetails = false);


        Task<IdentityUserDto> CreateAsync(IdentityUserOrgCreateDto input);

        Task<IdentityUserDto> UpdateAsync(Guid id, IdentityUserOrgUpdateDto input);
    }
}
