using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Volo.Abp.IdentityServer
{
    [Serializable]
    public class IdentityResourceDto : ExtensibleEntityDto<Guid>
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public bool Enabled { get; set; }

        public bool Required { get; set; }

        public bool Emphasize { get; set; }

        public bool ShowInDiscoveryDocument { get; set; }
        
        public List<IdentityClaimDto> UserClaims { get; set; }

        public Dictionary<string, string> Properties { get; set; }
    }
}