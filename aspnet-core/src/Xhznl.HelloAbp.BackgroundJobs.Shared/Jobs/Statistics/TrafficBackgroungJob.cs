using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AuditLogging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;
using Volo.Abp.Emailing.Smtp;
using Volo.Abp.Security.Encryption;
using Volo.Abp.Settings;
using Volo.Abp.Threading;

namespace Xhznl.HelloAbp.Jobs.Statistics
{
    public class TrafficBackgroungJob : AsyncBackgroundJob<TrafficArgs>, ITransientDependency
    {
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly ISmtpEmailSender _smtpEmailSender;
        private readonly ISmtpEmailSenderConfiguration  _smtpEmailSenderConfiguration;
        private readonly IStringEncryptionService _encryptionService;
        public TrafficBackgroungJob(IAuditLogRepository auditLogRepository,
            ISmtpEmailSender smtpEmailSender,
            ISmtpEmailSenderConfiguration smtpEmailSenderConfiguration,
            IStringEncryptionService encryptionService)
        {
            _auditLogRepository = auditLogRepository;
            _smtpEmailSender = smtpEmailSender;
            _smtpEmailSenderConfiguration = smtpEmailSenderConfiguration;
            _encryptionService = encryptionService;
        }

        public override async Task ExecuteAsync(TrafficArgs args)
        {
            var errorsCount = await _auditLogRepository.GetCountAsync(
                startTime: DateTime.Now.Date,
                endTime: DateTime.Now.Date.AddDays(1),
                httpStatusCode: System.Net.HttpStatusCode.InternalServerError);

            var errors = await _auditLogRepository.GetListAsync(
                startTime: DateTime.Now.Date,
                endTime: DateTime.Now.Date.AddDays(1),
                httpStatusCode: System.Net.HttpStatusCode.InternalServerError);

            var appErrors = errors.GroupBy(l => l.ApplicationName)
                .Select(l => new
                {
                    ApplicationName = l.Key,
                    Count = l.Count()
                });
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"今日总异常数:{errorsCount}");
            foreach (var error in appErrors)
            {
                sb.AppendLine($"{error.ApplicationName}异常数:${error.Count}");
            }

            var email = await GetEmailSetting();
            await _smtpEmailSender.QueueAsync(
                from: email.from,
                to: email.to,
                subject: email.subject,
                sb.ToString()
            );
        }

        private async Task<(string from, string to, string subject)> GetEmailSetting()
        {
            var from = await _smtpEmailSenderConfiguration.GetDefaultFromAddressAsync();
            var to = "xianghl@cy-coo.com";
            var subject = "每日异常量统计";
            return (from, to, subject);
        }
    }
}