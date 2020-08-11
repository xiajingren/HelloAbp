using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xhznl.HelloAbp.Data;
using Volo.Abp.DependencyInjection;

namespace Xhznl.HelloAbp.EntityFrameworkCore
{
    public class EntityFrameworkCoreHelloAbpDbSchemaMigrator
        : IHelloAbpDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreHelloAbpDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the HelloAbpMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<HelloAbpMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}