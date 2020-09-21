using Volo.Abp.Application.Dtos;

namespace Xhznl.DataDictionary.BaseData.DataDictionaryManagement.Dto
{
    public class GetDictionaryInputDto: PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}