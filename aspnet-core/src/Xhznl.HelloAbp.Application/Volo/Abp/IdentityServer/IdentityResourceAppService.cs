using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.IdentityServer.IdentityResources;

namespace Volo.Abp.IdentityServer
{
    [RemoteService(false)]
    public class IdentityResourceAppService : IdentityServerAppServiceBase, IIdentityResourceAppService
    {
        public IdentityResourceAppService(IIds4IdentityResourceRepository identityResourceRepository)
        {
            IdentityResourceRepository = identityResourceRepository;
        }

        protected IIds4IdentityResourceRepository IdentityResourceRepository { get; }

        public async Task<IdentityResourceDto> GetAsync(Guid id)
        {
            return MapIdentityResourceToDto(await IdentityResourceRepository.GetAsync(id));
        }

        public async Task<PagedResultDto<IdentityResourceDto>> GetListAsync(GetIdentityResourceListInput input)
        {
            var cnt = await IdentityResourceRepository.GetCountAsync(input.Filter);
            var list = await IdentityResourceRepository.GetListAsync(input.Sorting, input.SkipCount,
                input.MaxResultCount, input.Sorting,
                includeDetails: true);
            return new PagedResultDto<IdentityResourceDto>(cnt, MapListIdentityResourceToListDto(list));
        }

        public Task<IdentityResourceDto> CreateAsync(CreateIdentityResourceDto input)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResourceDto> UpdateAsync(Guid id, UpdateIdentityResourceDto input)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        protected virtual IdentityResourceDto MapIdentityResourceToDto(IdentityResource identityResource)
        {
            var identityResourceDto = ObjectMapper.Map<IdentityResource, IdentityResourceDto>(identityResource);
            return identityResourceDto;
        }

        protected virtual List<IdentityResourceDto> MapListIdentityResourceToListDto(
            List<IdentityResource> identityResources)
        {
            var list = ObjectMapper.Map<List<IdentityResource>, List<IdentityResourceDto>>(identityResources);
            return list;
        }
    }
}