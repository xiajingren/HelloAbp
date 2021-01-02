using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;

namespace Volo.Abp.IdentityServer.IdentityResources
{
    public class Ids4IdentityResourceRepository : IdentityResourceRepository, IIds4IdentityResourceRepository
    {
        public Ids4IdentityResourceRepository(IDbContextProvider<IIdentityServerDbContext> dbContextProvider) : base(
            dbContextProvider)
        {
        }

        public Task<long> GetCountAsync(string filter, CancellationToken cancellationToken = default)
        {
            return DbSet.WhereIf(!filter.IsNullOrWhiteSpace(), x => x.Name.Contains(filter) ||
                                                                    x.Description.Contains(filter) ||
                                                                    x.DisplayName.Contains(filter))
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }
    }
}