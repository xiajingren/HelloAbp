using System.Threading;
using System.Threading.Tasks;

namespace Volo.Abp.IdentityServer.IdentityResources
{
    public interface IIds4IdentityResourceRepository : IIdentityResourceRepository
    {
        Task<long> GetCountAsync(string filter, CancellationToken cancellationToken = default);
    }
}