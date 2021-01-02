using System;

namespace Volo.Abp.IdentityServer
{
    [Serializable]
    public class ApiScopeClaimDto:IdentityClaimDto
    {
        public string Name { get; set; }
    }
}