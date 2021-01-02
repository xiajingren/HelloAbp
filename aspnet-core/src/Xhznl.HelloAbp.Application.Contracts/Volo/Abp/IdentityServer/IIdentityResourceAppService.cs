using System;
using Volo.Abp.Application.Services;

namespace Volo.Abp.IdentityServer
{
    public interface IIdentityResourceAppService:ICrudAppService<IdentityResourceDto,Guid,GetIdentityResourceListInput,CreateIdentityResourceDto,
        UpdateIdentityResourceDto>
    {
        
    }
}