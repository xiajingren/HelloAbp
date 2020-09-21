using AutoMapper;
using Volo.Abp.AutoMapper;
using Xhznl.DataDictionary.BaseData.DataDictionaryManagement.Dto;

namespace Xhznl.DataDictionary
{
    public class DataDictionaryApplicationAutoMapperProfile : Profile
    {
        public DataDictionaryApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<DataDictionary, DictionaryDto>();
            CreateMap<CreateDataDictionaryDto, DataDictionary>()
                .Ignore(d => d.TenantId)
                .Ignore(d => d.ConcurrencyStamp)
                .IgnoreSoftDeleteProperties();
            CreateMap<UpdateDataDictionaryDto, DataDictionary>()
                .Ignore(d => d.TenantId)
                .Ignore(d => d.ConcurrencyStamp)
                .IgnoreSoftDeleteProperties();

            CreateMap<DataDictionaryDetail, DictionaryDetailDto>();
            CreateMap<CreateDataDictionaryDetailDto, DataDictionaryDetail>()
                .Ignore(d => d.TenantId)
                .Ignore(d => d.ConcurrencyStamp)
                .IgnoreSoftDeleteProperties();
            CreateMap<UpdateDataDictionaryDetailDto, DataDictionaryDetail>()
                .Ignore(d => d.TenantId)
                .Ignore(d => d.ConcurrencyStamp)
                .IgnoreSoftDeleteProperties();
        }
    }
}