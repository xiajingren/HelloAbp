using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Volo.Abp.AuditLogging
{
    public interface IAuditLogAppService : IReadOnlyAppService<AuditLogDto, Guid, GetAuditLogDto>, IDeleteAppService<Guid>
    {
        Task DeleteManyAsync(params Guid[] ids);
    }
}
