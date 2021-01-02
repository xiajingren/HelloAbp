using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Volo.Abp.IdentityServer
{
    [RemoteService(Name = IdentityServerRemoteServiceConsts.RemoteServiceName)]
    [Area("identity-server")]
    [ControllerName("ApiResource")]
    [Route("api/identity-server/api-resources")]
    public class ApiResourceController : AbpController, IApiResourceAppService
    {
        public ApiResourceController(IApiResourceAppService apiResourceAppService)
        {
            ApiResourceAppService = apiResourceAppService;
        }

        protected IApiResourceAppService ApiResourceAppService { get; }

        [HttpGet("{id}")]
        public Task<ApiResourceDetailsDto> GetAsync(Guid id)
        {
            return ApiResourceAppService.GetAsync(id);
        }

        [HttpGet]
        public Task<PagedResultDto<ApiResourceDto>> GetListAsync(GetApiResourceListInput input)
        {
            return ApiResourceAppService.GetListAsync(input);
        }

        [HttpPost]
        public Task<ApiResourceDetailsDto> CreateAsync(CreateApiResourceDto input)
        {
            return ApiResourceAppService.CreateAsync(input);
        }

        [HttpPut("{id}")]
        public Task<ApiResourceDetailsDto> UpdateAsync(Guid id, UpdateApiResourceDto input)
        {
            return ApiResourceAppService.UpdateAsync(id, input);
        }

        [HttpDelete("{id}")]
        public Task DeleteAsync(Guid id)
        {
            return ApiResourceAppService.DeleteAsync(id);
        }
    }
}