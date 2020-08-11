using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace Xhznl.HelloAbp.EntityFrameworkCore
{
    public static class HelloAbpDbContextModelCreatingExtensions
    {
        public static void ConfigureHelloAbp(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(HelloAbpConsts.DbTablePrefix + "YourEntities", HelloAbpConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});
        }
    }
}