using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;
using Xhznl.HelloAbp.Data;

namespace Xhznl.HelloAbp.EntityFrameworkCore
{
    class EntityFrameworkCoreHelloAbpIdsDbSchemaMigrator : IHelloAbpDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreHelloAbpIdsDbSchemaMigrator(
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
                .GetRequiredService<HelloAbpIdsMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}