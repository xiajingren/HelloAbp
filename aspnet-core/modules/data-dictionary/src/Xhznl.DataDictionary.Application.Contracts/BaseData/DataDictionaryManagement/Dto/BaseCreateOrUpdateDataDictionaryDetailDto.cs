using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectExtending;

namespace Xhznl.DataDictionary.BaseData.DataDictionaryManagement.Dto
{
    public abstract class BaseCreateOrUpdateDataDictionaryDetailDto
    {
        public virtual Guid Pid { get; set; }

        [Required]
        public virtual string Label { get; set; }

        [Required]
        public virtual string Value { get; set; }

        public virtual short Sort { get; set; }
    }
}