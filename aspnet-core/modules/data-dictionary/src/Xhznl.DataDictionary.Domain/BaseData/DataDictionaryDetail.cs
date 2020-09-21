using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Xhznl.DataDictionary
{
    public class DataDictionaryDetail : AuditedAggregateRoot<Guid>, IMultiTenant, ISoftDelete
    {
        public Guid Pid { get; private set; }

        public string Label { get; private set; }

        public string Value { get; private set; }

        public short Sort { get; private set; }

        public bool IsDeleted { get; set; }

        public Guid? TenantId { get; private set; }

        protected DataDictionaryDetail()
        {

        }

        public DataDictionaryDetail(Guid id,
            [NotNull] Guid pid,
            [CanBeNull] Guid? tenantId,
            [NotNull] string label,
            [NotNull] string value,
            short sort = 0)
        {
            Check.NotNull(pid, nameof(pid), "parent id can not be null");
            Check.NotNullOrEmpty(label, nameof(label), DataDictionaryConsts.MaxNameLength);
            Check.NotNullOrEmpty(value, nameof(value), DataDictionaryConsts.MaxNotesLength);

            Id = id;
            Pid = pid;
            TenantId = tenantId;
            Label = label;
            Value = value;
            Sort = sort;
        }
    }
}