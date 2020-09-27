using System.Threading.Tasks;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.AspNetCore;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;
using Xhznl.HelloAbp.EntityFrameworkCore;
using Xhznl.HelloAbp.Jobs;
using Xhznl.HelloAbp.Jobs.ChinaRegion;
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
            //后台作业
            // var manager = context.ServiceProvider.GetRequiredService<IBackgroundJobManager>();
            // manager.EnqueueAsync(new TrafficArgs(100));
            AddChinaRegionJob(context);
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

        private void AddTrafficJob(ApplicationInitializationContext context)
        {
            //周期性作业
            var trafficBackgroungJob = context.ServiceProvider.GetRequiredService<TrafficBackgroungJob>();
            RecurringJob.AddOrUpdate("每天服务异常量统计",
                () => trafficBackgroungJob.ExecuteAsync(null),
                HelloAbpCronType.Minute());
        }

        private async Task AddChinaRegionJob(ApplicationInitializationContext context)
        {
            var regionJob = context.ServiceProvider.GetRequiredService<CrawlingChinaRegionJob>();
            var provinces = await regionJob.GetProvincesAsync();
            foreach (var dic in provinces)
            {
                RecurringJob.AddOrUpdate($"{dic.Key}--每个月同步",
                    () => regionJob.ExecuteAsync(new ChinaRegionArgs()
                    {
                        Province = dic.Key,
                        Href = dic.Value
                    }),
                    HelloAbpCronType.Month());
            }
        }
    }
}