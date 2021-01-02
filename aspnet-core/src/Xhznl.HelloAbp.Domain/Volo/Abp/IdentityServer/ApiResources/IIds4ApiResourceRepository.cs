using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.IdentityServer.ApiResources;

namespace Volo.Abp.IdentityServer.ApiResources
{
    public interface IIds4ApiResourceRepository : IApiResourceRepository
    {
        Task<long> GetCountAsync(string filter, CancellationToken cancellationToken = default);
    }
}