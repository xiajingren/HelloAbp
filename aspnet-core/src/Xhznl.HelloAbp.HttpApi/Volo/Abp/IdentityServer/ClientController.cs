using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity;

namespace Volo.Abp.IdentityServer
{
    [RemoteService(true, Name = IdentityServerRemoteServiceConsts.RemoteServiceName)]
    [Area("identity-server")]
    [ControllerName("Client")]
    [Route("api/identity-server/clients")]
    public class ClientController : AbpController, IClientAppService
    {
        protected IClientAppService ClientAppService { get; }

        public ClientController(IClientAppService clientAppService)
        {
            ClientAppService = clientAppService;
        }

        [Route("{id}")]
        [HttpGet]
        public Task<ClientDto> GetAsync(Guid id)
        {
            return ClientAppService.GetAsync(id);
        }

        [HttpGet]
        public Task<PagedResultDto<ClientDto>> GetListAsync(GetClientListInput input)
        {
            return ClientAppService.GetListAsync(input);
        }

        [HttpPost]
        public Task<ClientDto> CreateAsync(CreateClientDto input)
        {
            return ClientAppService.CreateAsync(input);
        }

        [HttpPut]
        public Task<ClientDto> UpdateAsync(Guid id, UpdateClientDto input)
        {
            return ClientAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        public Task DeleteAsync(Guid id)
        {
            return ClientAppService.DeleteAsync(id);
        }
    }
}