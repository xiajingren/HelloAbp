using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xhznl.HelloAbp.BackgroundJobs.Jobs.Statistics;

namespace Xhznl.HelloAbp.Jobs
{
    public static class HelloAbpBackgroundJobsExtensions
    {
        public static IServiceProvider UseTrafficJob(this IServiceProvider services)
        {
            var job = services.GetRequiredService<IHelloAbpBackgroungJob>();
            RecurringJob.AddOrUpdate("每日异常日志统计", () => job.ExecuteAsync(), CronType.Minute());
            return services;
        }
    }
}
