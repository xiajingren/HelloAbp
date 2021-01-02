using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.IdentityServer.ApiResources;

namespace Volo.Abp.IdentityServer
{
    [Authorize(IdentityServerPermissions.ApiResources.Default)]
    [RemoteService(false)]
    public class ApiResourceAppService : IdentityServerAppServiceBase, IApiResourceAppService
    {
        public ApiResourceAppService(IIds4ApiResourceRepository apiResourceRepository)
        {
            ApiResourceRepository = apiResourceRepository;
        }

        protected IIds4ApiResourceRepository ApiResourceRepository { get; }

        public async Task<ApiResourceDetailsDto> GetAsync(Guid id)
        {
            return MapApiResourceToDto(await ApiResourceRepository.GetAsync(id));
        }

        public async Task<PagedResultDto<ApiResourceDto>> GetListAsync(GetApiResourceListInput input)
        {
            var cnt = await ApiResourceRepository.GetCountAsync(input.Filter);
            var list = await ApiResourceRepository.GetListAsync(input.Sorting, input.SkipCount, input.MaxResultCount,
                input.Filter);
            return new PagedResultDto<ApiResourceDto>(cnt, MapListApiResourceToListDto(list));
        }

        [Authorize(IdentityServerPermissions.ApiResources.Create)]
        public Task<ApiResourceDetailsDto> CreateAsync(CreateApiResourceDto input)
        {
            throw new NotImplementedException();
        }

        [Authorize(IdentityServerPermissions.ApiResources.Update)]
        public Task<ApiResourceDetailsDto> UpdateAsync(Guid id, UpdateApiResourceDto input)
        {
            throw new NotImplementedException();
        }

        [Authorize(IdentityServerPermissions.ApiResources.Delete)]
        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        protected virtual ApiResourceDetailsDto MapApiResourceToDto(ApiResource apiResource)
        {
            var apiResourceDetailsDto = ObjectMapper.Map<ApiResource, ApiResourceDetailsDto>(apiResource);
            return apiResourceDetailsDto;
        }

        protected virtual List<ApiResourceDto> MapListApiResourceToListDto(List<ApiResource> apiResources)
        {
            var list = ObjectMapper.Map<List<ApiResource>, List<ApiResourceDto>>(apiResources);
            return list;
        }
    }
}