using System;
using Volo.Abp.Application.Dtos;

namespace Xhznl.DataDictionary.BaseData.DataDictionaryManagement.Dto
{
    public class DictionaryDto: EntityDto<Guid>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public short Short { get; set; }
    }
}