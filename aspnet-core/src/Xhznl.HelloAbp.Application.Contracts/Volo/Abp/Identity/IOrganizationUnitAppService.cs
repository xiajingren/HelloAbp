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
        Task<ListResultDto<OrganizationUnitDto>> GetRootListAsync();

        Task<OrganizationUnitDto> GetDetailsAsync(Guid id);

        Task<PagedResultDto<OrganizationUnitDto>> GetListDetailsAsync(GetOrganizationUnitInput input);

        Task<ListResultDto<OrganizationUnitDto>> GetAllListAsync(GetAllOrgnizationUnitInput input);

        Task<ListResultDto<OrganizationUnitDto>> GetAllListDetailsAsync(GetAllOrgnizationUnitInput input);

        Task<List<OrganizationUnitDto>> GetChildrenAsync(Guid parentId);

        Task<string> GetNextChildCodeAsync(Guid? parentId);

        Task MoveAsync(Guid id, Guid? parentId);

        Task<PagedResultDto<IdentityUserDto>> GetUsersAsync(Guid? ouId,GetIdentityUsersInput userInput);
        Task<PagedResultDto<IdentityRoleDto>> GetRolesAsync(Guid? ouId, PagedAndSortedResultRequestDto roleInput);
    }
}
