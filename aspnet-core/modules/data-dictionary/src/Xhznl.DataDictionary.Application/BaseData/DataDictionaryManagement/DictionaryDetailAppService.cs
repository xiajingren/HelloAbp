using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Xhznl.DataDictionary;
using Xhznl.DataDictionary.BaseData.DataDictionaryManagement.Dto;
using Xhznl.DataDictionary.Localization;
using Xhznl.DataDictionary.Permissions;

namespace Xhznl.DataDictionary.BaseData.DataDictionaryManagement
{
    [Authorize(DataDictionaryPermissions.DataDictionary.Default)]
    public class DictionaryDetailAppService :
        CrudAppService<DataDictionaryDetail, DictionaryDetailDto, Guid, GetDictionaryDetailInputDto,
            CreateDataDictionaryDetailDto, UpdateDataDictionaryDetailDto>, IDictionaryDetailAppService
    {
        protected IRepository<DataDictionary, Guid> MasterRepository { get; }

        public DictionaryDetailAppService(IRepository<DataDictionaryDetail, Guid> repository,
            IRepository<DataDictionary, Guid> masterRepository) : base(repository)
        {
            MasterRepository = masterRepository;
            LocalizationResource = typeof(DataDictionaryResource);
            ObjectMapperContext = typeof(DataDictionaryApplicationModule);
        }

        [Authorize(DataDictionaryPermissions.DataDictionaryDetail.Create)]
        public override async Task<DictionaryDetailDto> CreateAsync(CreateDataDictionaryDetailDto input)
        {
            var master = await MasterRepository.FindAsync(d => d.Id == input.Pid);
            if (master == null)
            {
                throw new UserFriendlyException(message: L["DetailCreateMasterNullMessage"],
                    AbpDataDictionaryErrorCodes.DictionaryIsNotExist);
            }

            var exist = await Repository.FindAsync(d => d.Label == input.Label);
            if (exist != null)
            {
                throw new UserFriendlyException(message: L["HasCreatedMessage", input.Label],
                    AbpDataDictionaryErrorCodes.DictionaryDetailsIsExist);
            }

            var result = await Repository.InsertAsync(new DataDictionaryDetail(
                GuidGenerator.Create(),
                input.Pid,
                CurrentTenant.Id,
                input.Label,
                input.Value,
                input.Sort)
            );
            return MapDataDictionaryDetailToDto(result);
        }

        [Authorize(DataDictionaryPermissions.DataDictionaryDetail.Update)]
        public override async Task<DictionaryDetailDto> UpdateAsync(Guid id, UpdateDataDictionaryDetailDto input)
        {
            var detail = await Repository.GetAsync(id);
            detail.SetValue(input.Label, input.Value);
            detail.SetSort(input.Sort);
            var result = await Repository.UpdateAsync(detail, true);
            return MapDataDictionaryDetailToDto(result);
        }

        [Authorize(DataDictionaryPermissions.DataDictionaryDetail.Delete)]
        public async Task DeleteAsync(List<Guid> ids)
        {
            foreach (var id in ids)
            {
                await Repository.DeleteAsync(id);
            }
        }

        protected virtual DictionaryDetailDto MapDataDictionaryDetailToDto(DataDictionaryDetail detail)
        {
            var detailDto = base.ObjectMapper.Map<DataDictionaryDetail, DictionaryDetailDto>(detail);
            return detailDto;
        }

        protected virtual List<DictionaryDetailDto> MapListDataDictionaryDetailToListDto(
            List<DataDictionaryDetail> details)
        {
            var list = base.ObjectMapper.Map<List<DataDictionaryDetail>, List<DictionaryDetailDto>>(details);
            return list;
        }

        protected override IQueryable<DataDictionaryDetail> CreateFilteredQuery(GetDictionaryDetailInputDto input)
        {
            return Repository.Where(p => p.Pid == input.Pid);
        }
    }
}