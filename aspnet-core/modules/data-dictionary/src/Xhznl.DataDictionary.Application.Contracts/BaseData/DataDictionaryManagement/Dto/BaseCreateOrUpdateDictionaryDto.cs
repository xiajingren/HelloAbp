using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Validation;

namespace Xhznl.DataDictionary.BaseData.DataDictionaryManagement.Dto
{
    public abstract class BaseCreateOrUpdateDictionaryDto : ExtensibleAuditedEntityDto<Guid>
    {
        [Required]
        [DynamicStringLength(typeof(DataDictionaryConsts), nameof(DataDictionaryConsts.MaxNameLength))]
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual short Short { get; set; }
    }
}