using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Volo.Abp.Identity
{
    [RemoteService(Name = IdentityRemoteServiceConsts.RemoteServiceName)]
    [Area("identity")]
    [ControllerName("Organization")]
    [Route("api/identity/organizations")]
    public class OrganizationUnitController : AbpController, IOrganizationUnitAppService
    {
        protected IOrganizationUnitAppService UnitAppService { get; }
        public OrganizationUnitController(IOrganizationUnitAppService unitAppService)
        {
            UnitAppService = unitAppService;
        }

        [HttpPost]
        public virtual Task<OrganizationUnitDto> CreateAsync(OrganizationUnitCreateDto input)
        {
            return UnitAppService.CreateAsync(input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return UnitAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("all")]
        public virtual Task<ListResultDto<OrganizationUnitDto>> GetAllListAsync(GetAllOrgnizationUnitInput input)
        {
            return UnitAppService.GetAllListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<OrganizationUnitDto> GetAsync(Guid id)
        {
            return UnitAppService.GetAsync(id);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<OrganizationUnitDto>> GetListAsync(GetOrganizationUnitInput input)
        {
            return UnitAppService.GetListAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<OrganizationUnitDto> UpdateAsync(Guid id, OrganizationUnitUpdateDto input)
        {
            return UnitAppService.UpdateAsync(id, input);
        }

        [HttpPut]
        [Route("move")]
        public Task MoveAsync(Guid id, Guid? parentId)
        {
            return UnitAppService.MoveAsync(id, parentId);
        }

        [HttpGet]
        [Route("{id}/details")]
        public Task<OrganizationUnitDto> GetDetailsAsync(Guid id)
        {
            return UnitAppService.GetDetailsAsync(id);
        }

        [HttpGet]
        [Route("details")]
        public Task<PagedResultDto<OrganizationUnitDto>> GetListDetailsAsync(GetOrganizationUnitInput input)
        {
            return UnitAppService.GetListDetailsAsync(input);
        }

        [HttpGet]
        [Route("all/details")]
        public Task<ListResultDto<OrganizationUnitDto>> GetAllListDetailsAsync(GetAllOrgnizationUnitInput input)
        {
            return UnitAppService.GetAllListDetailsAsync(input);
        }

        [HttpGet]
        [Route("children/{parentId}")]
        public Task<List<OrganizationUnitDto>> GetChildrenAsync(Guid parentId)
        {
            return UnitAppService.GetChildrenAsync(parentId);
        }

        [HttpGet]
        [Route("root")]
        public Task<ListResultDto<OrganizationUnitDto>> GetRootListAsync()
        {
            return UnitAppService.GetRootListAsync();
        }

        [HttpGet]
        [Route("next-code")]
        public Task<string> GetNextChildCodeAsync(Guid? parentId)
        {
            return UnitAppService.GetNextChildCodeAsync(parentId);
        }

        [HttpGet]
        [Route("users")]
        public Task<PagedResultDto<IdentityUserDto>> GetUsersAsync(Guid? ouId, GetIdentityUsersInput usersInput)
        {
            return UnitAppService.GetUsersAsync(ouId, usersInput);
        }

        [HttpGet]
        [Route("roles")]
        public Task<PagedResultDto<IdentityRoleDto>> GetRolesAsync(Guid? ouId, PagedAndSortedResultRequestDto roleInput)
        {
            return UnitAppService.GetRolesAsync(ouId, roleInput);
        }
    }
}
