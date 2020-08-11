using System.Threading.Tasks;

namespace Xhznl.HelloAbp.Data
{
    public interface IHelloAbpDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
