using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.IdentityServer.Clients;

namespace Volo.Abp.IdentityServer.Clients
{
    public interface IIds4ClientRepository : IClientRepository
    {
        Task<long> GetCountAsync(string filter, CancellationToken cancellationToken = default);
    }
}