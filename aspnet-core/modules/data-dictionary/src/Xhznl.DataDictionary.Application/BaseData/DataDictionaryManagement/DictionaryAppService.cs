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
    public class DictionaryAppService : CrudAppService<DataDictionary, DictionaryDto, Guid, GetDictionaryInputDto, CreateDataDictionaryDto, UpdateDataDictionaryDto>, IDictionaryAppService
    {
        public DictionaryAppService(IRepository<DataDictionary, Guid> repository) : base(repository)
        {
            LocalizationResource = typeof(DataDictionaryResource);
            ObjectMapperContext = typeof(DataDictionaryApplicationModule);
        }

        public async override Task<DictionaryDto> CreateAsync(CreateDataDictionaryDto input)
        {
            if ((await Repository.FindAsync(d => d.Name == input.Name)) != null)
            {
                throw new BusinessException(L["HasCreatedMessage", input.Name]);
            }
            return await base.CreateAsync(input);
        }

        public virtual async Task DeleteAsync(List<Guid> ids)
        {
            foreach (var id in ids)
            {
                await Repository.DeleteAsync(id);
            }
        }

        protected override IQueryable<DataDictionary> CreateFilteredQuery(GetDictionaryInputDto input)
        {
            return Repository.WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                d => d.Name.Contains(input.Filter) ||
                d.Description.Contains(input.Filter)
            );
        }
        #region override Policy
        protected override string UpdatePolicyName => DataDictionaryPermissions.DataDictionary.Update;
        protected override string CreatePolicyName => DataDictionaryPermissions.DataDictionary.Create;
        protected override string DeletePolicyName => DataDictionaryPermissions.DataDictionary.Delete;
        #endregion
    }
}