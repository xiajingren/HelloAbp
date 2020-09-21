using System;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Xhznl.DataDictionary
{
    public class DataDictionary : AuditedAggregateRoot<Guid>, IMultiTenant, ISoftDelete
    {
        public string Name { get; private set; }

        public string Description { get; private set; }

        public short Short { get; private set; }

        public bool IsDeleted { get; set; }

        public Guid? TenantId { get; private set; }

        protected DataDictionary()
        {

        }

        public DataDictionary(Guid id,
            [NotNull] string name,
            string description)
        {
            Check.NotNullOrEmpty(name, nameof(name), DataDictionaryConsts.MaxNameLength);

            Id = id;
            Name = name;
            Description = description;
        }
    }
}