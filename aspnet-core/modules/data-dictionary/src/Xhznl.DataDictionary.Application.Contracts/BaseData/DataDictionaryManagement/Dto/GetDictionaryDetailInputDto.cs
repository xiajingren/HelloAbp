using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Xhznl.DataDictionary.BaseData.DataDictionaryManagement.Dto
{
    public class GetDictionaryDetailInputDto: PagedAndSortedResultRequestDto
    {
        [Required]
        public Guid Pid { get; set; }
    }
}