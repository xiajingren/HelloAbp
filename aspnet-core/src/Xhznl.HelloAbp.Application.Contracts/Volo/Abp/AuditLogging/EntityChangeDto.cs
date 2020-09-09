using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;
using Volo.Abp.Data;

namespace Volo.Abp.AuditLogging
{
    public class EntityChangeDto:EntityDto<Guid>,IHasExtraProperties
    {
        public DateTime ChangeTime { get;  set; }
        public EntityChangeType ChangeType { get;  set; }
        public Guid? EntityTenantId { get;  set; }
        public string EntityId { get;  set; }
        public string EntityTypeFullName { get;  set; }
        public List<EntityPropertyChangeDto> PropertyChanges { get;  set; }
        public Dictionary<string, object> ExtraProperties { get;  set; }
    }
}
