using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;

namespace Volo.Abp.IdentityServer.ApiResources
{
    public class Ids4ApiResourceRepository : ApiResourceRepository, IIds4ApiResourceRepository
    {
        public Ids4ApiResourceRepository(IDbContextProvider<IIdentityServerDbContext> dbContextProvider) : base(
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