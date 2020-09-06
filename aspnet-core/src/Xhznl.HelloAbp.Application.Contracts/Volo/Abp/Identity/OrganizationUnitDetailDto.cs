using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Identity;

namespace Volo.Abp.Identity
{
    public class OrganizationUnitDetailDto : OrganizationUnitDto
    {
        public OrganizationUnitDetailDto()
        {
            Children = new List<OrganizationUnitDetailDto>();
        }
        public new List<OrganizationUnitDetailDto> Children { get; set; }
    }
}
