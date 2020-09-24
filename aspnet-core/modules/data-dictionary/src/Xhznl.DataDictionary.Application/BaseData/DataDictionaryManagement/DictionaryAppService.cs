using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Linq.Dynamic.Core;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Xhznl.DataDictionary.Permissions;
using Xhznl.DataDictionary.BaseData.DataDictionaryManagement.Dto;
using Microsoft.Extensions.Localization;
using Xhznl.DataDictionary.Localization;

namespace Xhznl.DataDictionary.BaseData.DataDictionaryManagement
{
    [Authorize(DataDictionaryPermissions.DataDictionary.Default)]
    public class DictionaryAppService :
        CrudAppService<DataDictionary, DictionaryDto, Guid, GetDictionaryInputDto, CreateDataDictionaryDto,
            UpdateDataDictionaryDto>, IDictionaryAppService
    {
        private readonly IRepository<DataDictionaryDetail, Guid> _dictionaryDetail;

        public DictionaryAppService(IRepository<DataDictionary, Guid> repository,
            IRepository<DataDictionaryDetail, Guid> dictionaryDetail) : base(repository)
        {
            _dictionaryDetail = dictionaryDetail;
            LocalizationResource = typeof(DataDictionaryResource);
            ObjectMapperContext = typeof(DataDictionaryApplicationModule);
        }

        [Authorize(DataDictionaryPermissions.DataDictionary.Create)]
        public override async Task<DictionaryDto> CreateAsync(CreateDataDictionaryDto input)
        {
            if ((await Repository.FindAsync(d => d.Name == input.Name)) != null)
            {
                throw new UserFriendlyException(L["HasCreatedMessage", input.Name],
                    AbpDataDictionaryErrorCodes.DictionaryIsExist);
            }

            var dic = new DataDictionary(
                GuidGenerator.Create(),
                input.Name,
                input.Description,
                input.Sort,
                CurrentTenant.Id
            );
            var result = await Repository.InsertAsync(dic, true);
            return MapDataDictionaryToDto(result);
        }

        [Authorize(DataDictionaryPermissions.DataDictionary.Update)]
        public override async Task<DictionaryDto> UpdateAsync(Guid id, UpdateDataDictionaryDto input)
        {
            var dic = await Repository.GetAsync(id);
            dic.SetName(input.Name);
            dic.SetDescription(input.Description);
            dic.SetSort(input.Sort);
            var result = await Repository.UpdateAsync(dic, true);
            return MapDataDictionaryToDto(result);
        }

        [Authorize(DataDictionaryPermissions.DataDictionary.Delete)]
        public virtual async Task DeleteAsync(List<Guid> ids)
        {
            foreach (var id in ids)
            {
                await DeleteAsync(id);
            }
        }

        [Authorize(DataDictionaryPermissions.DataDictionary.Delete)]
        public override async Task DeleteAsync(Guid id)
        {
            var detail = await Repository.GetAsync(id);
            if (detail == null)
            {
                return;
            }

            var details = (await _dictionaryDetail.GetListAsync())
                .Where(d => d.Pid == id)
                .ToList();
            foreach (var d in details)
            {
                await _dictionaryDetail.DeleteAsync(d.Id);
            }

            await Repository.DeleteAsync(id);
        }

        protected virtual DictionaryDto MapDataDictionaryToDto(DataDictionary dictionary)
        {
            var dictionaryDto = base.ObjectMapper.Map<DataDictionary, DictionaryDto>(dictionary);
            return dictionaryDto;
        }

        protected virtual List<DictionaryDto> MapListDictionaryToListDto(List<DataDictionary> dictionaries)
        {
            var list = base.ObjectMapper.Map<List<DataDictionary>, List<DictionaryDto>>(dictionaries);
            return list;
        }

        protected override IQueryable<DataDictionary> CreateFilteredQuery(GetDictionaryInputDto input)
        {
            return Repository.WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                d => d.Name.Contains(input.Filter) ||
                     d.Description.Contains(input.Filter)
            );
        }
    }
}