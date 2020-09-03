using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace Volo.Abp.Identity
{
    public class OrganizationUnitUpdateDto : OrganizationUnitCreateOrUpdateDtoBase, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; }
    }
}
