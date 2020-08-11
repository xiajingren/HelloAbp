using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;

namespace Xhznl.HelloAbp.EntityFrameworkCore
{
    [ConnectionStringName("AbpIdentityServer")]
    public class HelloAbpIdsMigrationsDbContext :
               AbpDbContext<HelloAbpIdsMigrationsDbContext>
    {
        public HelloAbpIdsMigrationsDbContext(
            DbContextOptions<HelloAbpIdsMigrationsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Include modules to your migration db context */

            builder.ConfigureIdentityServer();
        }
    }
}
