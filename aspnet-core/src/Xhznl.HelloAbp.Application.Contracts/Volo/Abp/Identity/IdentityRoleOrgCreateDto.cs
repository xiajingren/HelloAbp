using System;
using System.Collections.Generic;
using System.Text;

namespace Volo.Abp.Identity
{
    public class IdentityRoleOrgCreateDto : IdentityRoleCreateDto
    {
        public IdentityRoleOrgCreateDto()
        {

        }

        public Guid? OrgId { get; set; }
    }
}
