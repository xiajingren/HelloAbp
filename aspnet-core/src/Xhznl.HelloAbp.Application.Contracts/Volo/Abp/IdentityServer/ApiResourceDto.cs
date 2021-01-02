using System;
using System.Collections.Generic;

namespace Volo.Abp.IdentityServer
{
    [Serializable]
    public class ApiResourceDto : Application.Dtos.ExtensibleEntityDto<Guid>
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public bool Enabled { get; set; }

        public Dictionary<string, string> Properties { get; set; }
    }
}