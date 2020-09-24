using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Xhznl.DataDictionary.BaseData.DataDictionaryManagement.Dto;

namespace Xhznl.DataDictionary.BaseData.DataDictionaryManagement
{
    public interface IDictionaryAppService: ICrudAppService<DictionaryDto,Guid,GetDictionaryInputDto,CreateDataDictionaryDto,UpdateDataDictionaryDto>
    {
        Task DeleteAsync(List<Guid> ids);
    }
}