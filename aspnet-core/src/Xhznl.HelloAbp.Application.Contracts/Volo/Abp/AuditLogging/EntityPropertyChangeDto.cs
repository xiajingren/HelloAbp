using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Volo.Abp.AuditLogging
{
    public class EntityPropertyChangeDto : EntityDto<Guid>
    {
        public string NewValue { get; set; }
        public string OriginalValue { get; set; }
        public string PropertyName { get; set; }
        public string PropertyTypeFullName { get; set; }
    }
}
