using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Volo.Abp.IdentityServer
{
    [Serializable]
    public class ClientDto: ExtensibleEntityDto<Guid>
    {
        public string ClientId { get; set; }

        public string ClientName { get; set; }

        public string Description { get; set; }

        public bool Enabled { get; set; } = true;
    }
}