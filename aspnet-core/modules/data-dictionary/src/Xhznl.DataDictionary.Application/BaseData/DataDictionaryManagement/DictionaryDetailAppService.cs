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
    public class DictionaryDetailAppService : CrudAppService<DataDictionaryDetail, DictionaryDetailDto, Guid, GetDictionaryDetailInputDto, CreateDataDictionaryDetailDto, UpdateDataDictionaryDetailDto>, IDictionaryDetailAppService
    {
        protected IRepository<DataDictionary, Guid> MasterRepository { get; }

        public DictionaryDetailAppService(IRepository<DataDictionaryDetail, Guid> repository,
            IRepository<DataDictionary, Guid> masterRepository) : base(repository)
        {
            MasterRepository = masterRepository;
            LocalizationResource = typeof(DataDictionaryResource);
            ObjectMapperContext = typeof(DataDictionaryApplicationModule);
        }

        public async override Task<DictionaryDetailDto> CreateAsync(CreateDataDictionaryDetailDto input)
        {
            var master = await MasterRepository.FindAsync(d => d.Id == input.Pid);
            if (master == null)
            {
                throw new BusinessException(L["DetailCreateMasterNullMessage"]);
            }

            var exist = await Repository.FindAsync(d => d.Label == input.Label);
            if (exist != null)
            {
                throw new BusinessException(L["HasCreatedMessage", input.Label]);
            }

            return await base.CreateAsync(input);
        }

        public async Task DeleteAsync(List<Guid> ids)
        {
            foreach (var id in ids)
            {
                await Repository.DeleteAsync(id);
            }
        }

        protected override IQueryable<DataDictionaryDetail> CreateFilteredQuery(GetDictionaryDetailInputDto input)
        {
            return Repository.Where(p => p.Pid == input.Pid);

        }

        #region override Policy
        protected override string UpdatePolicyName => DataDictionaryPermissions.DataDictionaryDetail.Update;
        protected override string CreatePolicyName => DataDictionaryPermissions.DataDictionaryDetail.Create;
        protected override string DeletePolicyName => DataDictionaryPermissions.DataDictionaryDetail.Delete;
        #endregion
    }
}