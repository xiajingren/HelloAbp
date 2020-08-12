using Microsoft.EntityFrameworkCore;
using Xhznl.HelloAbp.Users;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;
using Volo.Abp.Users.EntityFrameworkCore;

namespace Xhznl.HelloAbp.EntityFrameworkCore
{
    /* This is your actual DbContext used on runtime.
     * It includes only your entities.
     * It does not include entities of the used modules, because each module has already
     * its own DbContext class. If you want to share some database tables with the used modules,
     * just create a structure like done for AppUser.
     *
     * Don't use this DbContext for database migrations since it does not contain tables of the
     * used modules (as explained above). See HelloAbpMigrationsDbContext for migrations.
     */
    [ConnectionStringName("Default")]
    public class HelloAbpDbContext : AbpDbContext<HelloAbpDbContext>
    {
        public DbSet<AppUser> Users { get; set; }

        /* Add DbSet properties for your Aggregate Roots / Entities here.
         * Also map them inside HelloAbpDbContextModelCreatingExtensions.ConfigureHelloAbp
         */

        public HelloAbpDbContext(DbContextOptions<HelloAbpDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Configure the shared tables (with included modules) here */

            builder.Entity<AppUser>(b =>
            {
                b.ToTable(AbpIdentityDbProperties.DbTablePrefix + "Users"); //Sharing the same table "AbpUsers" with the IdentityUser
                
                b.ConfigureByConvention();
                b.ConfigureAbpUser();

                /* Configure mappings for your additional properties
                 * Also see the HelloAbpEfCoreEntityExtensionMappings class
                 */

                b.Property(x => x.Avatar).IsRequired(false).HasMaxLength(AppUserConsts.MaxAvatarLength).HasColumnName(nameof(AppUser.Avatar));
                b.Property(x => x.Introduction).IsRequired(false).HasMaxLength(AppUserConsts.MaxIntroductionLength).HasColumnName(nameof(AppUser.Introduction));
            });

            /* Configure your own tables/entities inside the ConfigureHelloAbp method */

            builder.ConfigureHelloAbp();
        }
    }
}
