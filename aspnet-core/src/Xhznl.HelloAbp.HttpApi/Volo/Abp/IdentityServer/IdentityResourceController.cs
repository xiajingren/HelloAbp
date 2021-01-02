using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Volo.Abp.IdentityServer
{
    [RemoteService(Name = IdentityServerRemoteServiceConsts.RemoteServiceName)]
    [Area("identity-server")]
    [ControllerName("IdentityResource")]
    [Route("api/identity-server/identity-resources")]
    public class IdentityResourceController : AbpController, IIdentityResourceAppService
    {
        public IdentityResourceController(IIdentityResourceAppService identityResourceAppService)
        {
            IdentityResourceAppService = identityResourceAppService;
        }

        protected IIdentityResourceAppService IdentityResourceAppService { get; }
        [HttpGet("{id}")]
        public Task<IdentityResourceDto> GetAsync(Guid id)
        {
            return IdentityResourceAppService.GetAsync(id);
        }

        [HttpGet]
        public Task<PagedResultDto<IdentityResourceDto>> GetListAsync(GetIdentityResourceListInput input)
        {
            return IdentityResourceAppService.GetListAsync(input);
        }

        [HttpPost]
        public Task<IdentityResourceDto> CreateAsync(CreateIdentityResourceDto input)
        {
            return IdentityResourceAppService.CreateAsync(input);
        }

        [HttpPut("{id}")]
        public Task<IdentityResourceDto> UpdateAsync(Guid id, UpdateIdentityResourceDto input)
        {
            return IdentityResourceAppService.UpdateAsync(id, input);
        }

        [HttpDelete("id")]
        public Task DeleteAsync(Guid id)
        {
            return IdentityResourceAppService.DeleteAsync(id);
        }
    }
}