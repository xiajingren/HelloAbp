using System;
using System.Collections.Generic;

namespace Volo.Abp.IdentityServer
{
    [Serializable]
    public class ApiResourceDetailsDto : ApiResourceDto
    {
        public virtual List<ApiSecretDto> Secrets { get; set; }

        public virtual List<ApiScopeDto> Scopes { get; set; }

        public virtual List<ApiResourceClaimDto> UserClaims { get; set; }
    }
}