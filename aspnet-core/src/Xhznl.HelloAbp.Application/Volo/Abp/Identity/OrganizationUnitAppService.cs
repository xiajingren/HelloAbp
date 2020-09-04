using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
                await UnitRepository.GetAsync(id)
            );
        }

        public virtual async Task<OrganizationUnitDto> GetDetailsAsync(Guid id)
        {
            var ou = await UnitRepository.GetAsync(id);
            var ouDto = ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(ou);
            await TraverseTreeAsync(ouDto, ouDto.Children);
            return ouDto;
        }

        public virtual async Task<ListResultDto<OrganizationUnitDto>> GetRootListAsync()
        {
            //TODO:Consider submitting to ABP to get the ou root node PR
            var root = ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(await UnitRepository.GetChildrenAsync(null));
            foreach (var item in root)
            {
                if ((await UnitRepository.GetChildrenAsync(item.Id)).Count == 0)
                {
                    item.SetLeaf();
                }
            }
            return new PagedResultDto<OrganizationUnitDto>(
                root.Count,
                root
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

        public virtual async Task<PagedResultDto<OrganizationUnitDto>> GetListDetailsAsync(GetOrganizationUnitInput input)
        {
            var count = await UnitRepository.GetCountAsync();
            var list = await UnitRepository.GetListAsync(input.Sorting, input.MaxResultCount, input.SkipCount);
            var listDto = ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(list);
            foreach (var ouDto in listDto)
            {
                await TraverseTreeAsync(ouDto, ouDto.Children);
            }
            return new PagedResultDto<OrganizationUnitDto>(
                count,
                listDto
            );
        }

        public virtual async Task<ListResultDto<OrganizationUnitDto>> GetAllListAsync(GetAllOrgnizationUnitInput input)
        {
            var root = await GetRootListAsync();
            foreach (var ouDto in root.Items)
            {
                await TraverseTreeAsync(ouDto, ouDto.Children);
            }
            return root;
        }

        public virtual async Task<ListResultDto<OrganizationUnitDto>> GetAllListDetailsAsync(GetAllOrgnizationUnitInput input)
        {
            var root = await GetRootListAsync();
            foreach (var ouDto in root.Items)
            {
                await TraverseTreeAsync(ouDto, ouDto.Children);
            }
            return root;
        }

        public Task<string> GetNextChildCodeAsync(Guid? parentId)
        {
            return UnitManager.GetNextChildCodeAsync(parentId);
        }

        public virtual async Task<List<OrganizationUnitDto>> GetChildrenAsync(Guid parentId)
        {
            //TODO:How to set is a leaf node when lazy loading
            var list = ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(await UnitManager.FindChildrenAsync(parentId));
            foreach (var item in list)
            {
                if ((await UnitRepository.GetChildrenAsync(item.Id)).Count == 0)
                {
                    item.SetLeaf();
                }
            }
            return list;
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

        public async Task MoveAsync(Guid id, Guid? parentId)
        {
            var ou = await UnitRepository.GetAsync(id);
            if (ou == null)
            {
                return;
            }
            await UnitManager.MoveAsync(id, parentId);
        }

        protected virtual async Task TraverseTreeAsync(OrganizationUnitDto dto, List<OrganizationUnitDto> children)
        {
            if (dto.Children.Count == 0)
            {
                children = await GetChildrenAsync(dto.Id);
                dto.Children.AddRange(children);
            }
            if (children == null || !children.Any())
            {
                await Task.CompletedTask;
                return;
            }
            foreach (var child in children)
            {
                var next = await GetChildrenAsync(child.Id);
                child.Children.AddRange(next);
                await TraverseTreeAsync(dto, child.Children);
            }
        }


    }
}
