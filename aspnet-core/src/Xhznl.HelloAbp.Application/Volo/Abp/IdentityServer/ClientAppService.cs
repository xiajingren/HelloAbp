using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer.Clients;

namespace Volo.Abp.IdentityServer
{
    [RemoteService(false)]
    [Authorize(IdentityServerPermissions.Clients.Default)]
    public class ClientAppService : IdentityServerAppServiceBase, IClientAppService
    {
        protected IIds4ClientRepository ClientRepository { get; }

        public ClientAppService(IIds4ClientRepository clientRepository)
        {
            ClientRepository = clientRepository;
        }

        public async Task<ClientDto> GetAsync(Guid id)
        {
            return MapClientToDto(await ClientRepository.GetAsync(id));
        }

        public async Task<PagedResultDto<ClientDto>> GetListAsync(GetClientListInput input)
        {
            var count = await ClientRepository.GetCountAsync(input.Filter);
            var list = await ClientRepository.GetListAsync(input.Sorting, input.SkipCount, input.MaxResultCount,
                input.Filter);
            return new PagedResultDto<ClientDto>(count, MapListClientToListDto(list));
        }

        [Authorize(IdentityServerPermissions.Clients.Create)]
        public Task<ClientDto> CreateAsync(CreateClientDto input)
        {
            throw new NotImplementedException();
        }

        [Authorize(IdentityServerPermissions.Clients.Update)]
        public Task<ClientDto> UpdateAsync(Guid id, UpdateClientDto input)
        {
            throw new NotImplementedException();
        }

        [Authorize(IdentityServerPermissions.Clients.Delete)]
        public Task DeleteAsync(Guid id)
        {
            return ClientRepository.DeleteAsync(id);
        }

        protected virtual ClientDto MapClientToDto(Client client)
        {
            var clientDto = base.ObjectMapper.Map<Client, ClientDto>(client);
            return clientDto;
        }

        protected virtual List<ClientDto> MapListClientToListDto(List<Client> clients)
        {
            var list = ObjectMapper.Map<List<Client>, List<ClientDto>>(clients);
            return list;
        }
    }
}