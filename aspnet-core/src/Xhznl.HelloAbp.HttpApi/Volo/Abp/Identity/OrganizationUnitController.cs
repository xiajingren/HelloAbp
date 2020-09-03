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
        public virtual Task<ListResultDto<OrganizationUnitDto>> GetAllListAsync()
        {
            return UnitAppService.GetAllListAsync();
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
    }
}
