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
            CreateMap<DataDictionary, DictionaryDto>()
                .MapExtraProperties();

            CreateMap<DataDictionaryDetail, DictionaryDetailDto>()
                .MapExtraProperties();
        }
    }
}