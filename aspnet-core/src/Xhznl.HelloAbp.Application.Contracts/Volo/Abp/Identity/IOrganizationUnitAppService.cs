using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Auditing;
using Volo.Abp.Identity;

namespace Volo.Abp.Identity
{
    public interface IOrganizationUnitAppService : ICrudAppService<OrganizationUnitDto, Guid, GetOrganizationUnitInput, OrganizationUnitCreateDto, OrganizationUnitUpdateDto>
    {
        Task<PagedResultDto<OrganizationUnitDto>> GetRootListAsync(GetOrganizationUnitInput input);

        Task<OrganizationUnitDetailDto> GetDetailsAsync(Guid id);

        Task<PagedResultDto<OrganizationUnitDetailDto>> GetListDetailsAsync(GetOrganizationUnitInput input);

        Task<ListResultDto<OrganizationUnitDto>> GetAllListAsync();

        Task<ListResultDto<OrganizationUnitDetailDto>> GetAllListDetailsAsync();

        Task<List<OrganizationUnitDetailDto>> GetChildrenAsync(Guid parentId);


        Task MoveAsync(Guid id, Guid? parentId);
    }
}
