using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectExtending;
using Xhznl.HelloAbp;

namespace Volo.Abp.Identity
{
    //TODO:No RemoteServiceAttribute here will conflict with the OrganizationUnitController
    [RemoteService(false)]
    [Authorize(HelloIdentityPermissions.OrganitaionUnits.Default)]
    public class OrganizationUnitAppService : IdentityAppServiceBase, IOrganizationUnitAppService
    {
        protected OrganizationUnitManager UnitManager { get; }
        protected IOrganizationUnitRepository UnitRepository { get; }
        public OrganizationUnitAppService(
            OrganizationUnitManager unitManager,
            IOrganizationUnitRepository unitRepository)
        {
            UnitManager = unitManager;
            UnitRepository = unitRepository;
        }

        public virtual async Task<OrganizationUnitDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(
                await UnitRepository.GetAsync(id, true)
            );
        }

        public virtual async Task<PagedResultDto<OrganizationUnitDto>> GetListAsync(GetOrganizationUnitInput input)
        {
            var count = await UnitRepository.GetCountAsync();
            var list = await UnitRepository.GetListAsync(input.Sorting, input.MaxResultCount, input.SkipCount);
            return new PagedResultDto<OrganizationUnitDto>(
                count,
                ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(list)
            );
        }

        public virtual async Task<ListResultDto<OrganizationUnitDto>> GetAllListAsync()
        {
            var list = await UnitRepository.GetListAsync();
            return new ListResultDto<OrganizationUnitDto>(
                ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(list)
            );
        }

        [Authorize(HelloIdentityPermissions.OrganitaionUnits.Create)]
        public virtual async Task<OrganizationUnitDto> CreateAsync(OrganizationUnitCreateDto input)
        {
            var ou = new OrganizationUnit(
                GuidGenerator.Create(),
                input.DisplayName,
                input.ParentId,
                CurrentTenant.Id
            )
            {

            };
            input.MapExtraPropertiesTo(ou);

            await UnitManager.CreateAsync(ou);
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(ou);
        }

        [Authorize(HelloIdentityPermissions.OrganitaionUnits.Update)]
        public virtual async Task<OrganizationUnitDto> UpdateAsync(Guid id, OrganizationUnitUpdateDto input)
        {
            var ou = await UnitRepository.GetAsync(id);
            ou.ConcurrencyStamp = input.ConcurrencyStamp;
            ou.DisplayName = input.DisplayName;

            input.MapExtraPropertiesTo(ou);

            await UnitManager.UpdateAsync(ou);
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(ou);
        }

        [Authorize(HelloIdentityPermissions.OrganitaionUnits.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            var ou = await UnitRepository.GetAsync(id, false);
            if (ou == null)
            {
                return;
            }
            await UnitManager.DeleteAsync(id);
        }
    }
}
