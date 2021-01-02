using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.IdentityServer.EntityFrameworkCore;

namespace Volo.Abp.IdentityServer.Clients
{
    public class Ids4ClientRepository : ClientRepository, IIds4ClientRepository
    {
        public Ids4ClientRepository(IDbContextProvider<IIdentityServerDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public Task<long> GetCountAsync(string filter, CancellationToken cancellationToken = default)
        {
            return DbSet.WhereIf(!filter.IsNullOrWhiteSpace(), x => x.ClientId.Contains(filter))
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }
    }
}