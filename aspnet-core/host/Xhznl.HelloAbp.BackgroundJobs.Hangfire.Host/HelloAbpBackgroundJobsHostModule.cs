using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.AspNetCore;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;
using Xhznl.HelloAbp.EntityFrameworkCore;
using Xhznl.HelloAbp.Jobs;
using Xhznl.HelloAbp.Jobs.Statistics;

namespace Xhznl.HelloAbp
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpAspNetCoreModule),
        typeof(HelloAbpEntityFrameworkCoreDbMigrationsModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(HelloAbpBackgroundJobSharedModule)
    )]
    public class HelloAbpBackgroundJobsHostModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            ConfigureHangfire(context);
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();

            app.UseHangfireServer();

            app.UseHangfireDashboard();
        }

        public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
        {
            var manager = context.ServiceProvider.GetRequiredService<IBackgroundJobManager>();
            manager.EnqueueAsync(new TrafficArgs(100));
        }

        private void ConfigureHangfire(ServiceConfigurationContext context)
        {
            var dbSchema = HelloAbpConsts.DbSchema + "hangfire";
            var configuration = context.Services.GetConfiguration();
            context.Services.AddHangfire(config =>
            {
                config.UseSqlServerStorage(configuration.GetConnectionString("Default"),
                    new Hangfire.SqlServer.SqlServerStorageOptions
                    {
                        SchemaName = dbSchema
                    });
            });
        }
    }
}