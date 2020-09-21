using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Xhznl.DataDictionary.BaseData.DataDictionaryManagement.Dto;

namespace Xhznl.DataDictionary.BaseData.DataDictionaryManagement
{
    public interface IDictionaryDetailAppService : ICrudAppService<DictionaryDetailDto, Guid, GetDictionaryDetailInputDto, CreateDataDictionaryDetailDto, UpdateDataDictionaryDetailDto>
    {
        Task DeleteAsync(List<Guid> ids);
    }
}