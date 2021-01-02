using System;
using Volo.Abp.Application.Services;

namespace Volo.Abp.IdentityServer
{
    public interface IApiResourceAppService : ICrudAppService<ApiResourceDetailsDto, ApiResourceDto, Guid,
        GetApiResourceListInput,
        CreateApiResourceDto, UpdateApiResourceDto>
    {
    }
}