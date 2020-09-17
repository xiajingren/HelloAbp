using Hangfire;
using Hangfire.Dashboard;
using Microsoft.Extensions.DependencyInjection;
using System;
using Volo.Abp;
using Volo.Abp.BackgroundJobs.Hangfire;
using Volo.Abp.Modularity;
using Xhznl.HelloAbp.Jobs;

namespace Xhznl.HelloAbp.Jobs
{
    [DependsOn(typeof(HelloAbpApplicationContractsModule))]
    public class HelloAbpBackgroundJobsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            ConfigureHangfire(context);
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();

            app.UseHangfireServer();

            app.UseHangfireDashboard(pathMatch: "/jobs", new DashboardOptions
            {
                DashboardTitle = "HelloAbp task scheduling center"
            });

            var service = context.ServiceProvider;

            service.UseTrafficJob();
        }

        private void ConfigureHangfire(ServiceConfigurationContext context)
        {
            var tablePrefix = HelloAbpConsts.DbTablePrefix + "hangfire";
            var configuration = context.Services.GetConfiguration().GetSection("ConnectionStrings");
            context.Services.AddHangfire(config =>
            {
                config.UseSqlServerStorage(configuration["AbpBackgroundJobs"] ??
                    configuration["Default"], new Hangfire.SqlServer.SqlServerStorageOptions
                    {

                    });
            });
        }
    }
}
