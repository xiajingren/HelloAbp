using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;

namespace Xhznl.HelloAbp.Volo.Abp.Identity
{
    [RemoteService(false)]
    [Authorize(HelloIdentityPermissions.ClaimTypes.Default)]
    public class IdentityClaimTypeAppService : IdentityAppServiceBase, IIdentityClaimTypeAppService
    {
        protected IdenityClaimTypeManager IdenityClaimTypeManager { get; }
        protected IIdentityClaimTypeRepository IdentityClaimTypeRepository { get; }

        public IdentityClaimTypeAppService(IdenityClaimTypeManager idenityClaimTypeManager, IIdentityClaimTypeRepository identityClaimTypeRepository)
        {
            IdenityClaimTypeManager = idenityClaimTypeManager;
            IdentityClaimTypeRepository = identityClaimTypeRepository;
        }

        public virtual async Task<ClaimTypeDto> GetAsync(Guid id)
        {
            var claimType = await this.IdentityClaimTypeRepository.GetAsync(id);
            return this.MapClaimTypeToDto(claimType);
        }

        public virtual async Task<PagedResultDto<ClaimTypeDto>> GetListAsync(GetIdentityClaimTypesInput input)
        {
            var count = await IdentityClaimTypeRepository.GetCountAsync(input.Filter);
            var source = await this.IdentityClaimTypeRepository.GetListAsync(input.Sorting, input.MaxResultCount, input.SkipCount, input.Filter);
            return new PagedResultDto<ClaimTypeDto>((long)count, this.MapListClaimTypeToListDto(source));
        }

        [Authorize(HelloIdentityPermissions.ClaimTypes.Create)]
        public virtual async Task<ClaimTypeDto> CreateAsync(CreateClaimTypeDto input)
        {
            var identityClaimType = base.ObjectMapper.Map<CreateClaimTypeDto, IdentityClaimType>(input);
            input.MapExtraPropertiesTo(identityClaimType);
            var claimType = await this.IdenityClaimTypeManager.CreateAsync(identityClaimType);
            return this.MapClaimTypeToDto(claimType);
        }

        [Authorize(HelloIdentityPermissions.ClaimTypes.Update)]
        public virtual async Task<ClaimTypeDto> UpdateAsync(Guid id, UpdateClaimTypeDto input)
        {
            var identityClaimType = await this.IdentityClaimTypeRepository.GetAsync(id);
            base.ObjectMapper.Map<UpdateClaimTypeDto, IdentityClaimType>(input, identityClaimType);
            input.MapExtraPropertiesTo(identityClaimType);
            var claimType = await this.IdenityClaimTypeManager.UpdateAsync(identityClaimType);
            return this.MapClaimTypeToDto(claimType);
        }

        [Authorize(HelloIdentityPermissions.ClaimTypes.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await this.IdentityClaimTypeRepository.DeleteAsync(id);
        }

        public virtual async Task<List<ClaimTypeDto>> GetAllListAsync()
        {
            var claimTypes = await IdentityClaimTypeRepository.GetListAsync();
            return this.MapListClaimTypeToListDto(claimTypes);
        }

        protected virtual ClaimTypeDto MapClaimTypeToDto(IdentityClaimType claimType)
        {
            var claimTypeDto = base.ObjectMapper.Map<IdentityClaimType, ClaimTypeDto>(claimType);
            claimTypeDto.ValueTypeAsString = claimTypeDto.ValueType.ToString();
            return claimTypeDto;
        }

        protected virtual List<ClaimTypeDto> MapListClaimTypeToListDto(List<IdentityClaimType> claimTypes)
        {
            var list = base.ObjectMapper.Map<List<IdentityClaimType>, List<ClaimTypeDto>>(claimTypes);
            foreach (var claimTypeDto in list)
            {
                claimTypeDto.ValueTypeAsString = claimTypeDto.ValueType.ToString();
            }
            return list;
        }
    }
}
