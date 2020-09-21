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

        public short Sort { get; private set; }

        public bool IsDeleted { get; set; }

        public Guid? TenantId { get; private set; }

        public void SetName([NotNull] string name)
        {
            Name = name;
        }

        public void SetDescription(string description)
        {
            Description = description;
        }

        public void SetSort(short sort)
        {
            Sort = sort;
        }

        protected DataDictionary()
        {
        }

        public DataDictionary(Guid id,
            [NotNull] string name,
            string description,
            short sort = 0,
            Guid? tenantid = null)
        {
            Check.NotNullOrEmpty(name, nameof(name), DataDictionaryConsts.MaxNameLength);

            Id = id;
            TenantId = tenantid;
            Name = name;
            Description = description;
            Sort = sort;
        }
    }
}