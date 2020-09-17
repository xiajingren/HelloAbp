using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AuditLogging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BackgroundJobs.Hangfire;
using Volo.Abp.DependencyInjection;
using Xhznl.HelloAbp.Jobs;

namespace Xhznl.HelloAbp.BackgroundJobs.Jobs.Statistics
{
    public class TrafficBackgroungJob : IHelloAbpBackgroungJob
    {
        private readonly IAuditLogAppService _auditLogAppService;
        public TrafficBackgroungJob(IAuditLogAppService auditLogAppService)
        {
            _auditLogAppService = auditLogAppService;
        }
        public async Task ExecuteAsync()
        {
            var errors = await _auditLogAppService.GetListAsync(new GetAuditLogDto
            {
                HttpStatusCode = System.Net.HttpStatusCode.InternalServerError,
                StartTime = DateTime.Now.Date,
                EndTime = DateTime.Now.Date.AddDays(1)
            });
            var appErrors = errors.Items.GroupBy(l => l.ApplicationName)
                .Select(l => new
                {
                    ApplicationName = l.Key,
                    Count = l.Count()
                });
            Console.WriteLine($"今日总异常数:{errors.TotalCount}");
            foreach (var error in appErrors)
            {
                Console.WriteLine($"{error.ApplicationName}异常数:${error.Count}");
            }
        }
    }
}
