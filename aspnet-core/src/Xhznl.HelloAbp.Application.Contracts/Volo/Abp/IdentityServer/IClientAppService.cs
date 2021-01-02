using System;
using Volo.Abp.Application.Services;

namespace Volo.Abp.IdentityServer
{
    public interface IClientAppService : ICrudAppService<ClientDetailsDto, ClientDto, Guid, GetClientListInput,
        CreateClientDto, UpdateClientDto>
    {
    }
}