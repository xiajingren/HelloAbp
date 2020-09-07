using System;
using System.Collections.Generic;
using System.Text;

namespace Volo.Abp.Identity
{
    public class IdentityUserOrgUpdateDto : IdentityUserUpdateDto
    {
        public List<Guid> OrgIds { get; set; }
    }
}
