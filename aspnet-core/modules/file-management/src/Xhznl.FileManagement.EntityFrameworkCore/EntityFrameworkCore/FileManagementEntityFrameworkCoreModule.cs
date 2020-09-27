using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Xhznl.FileManagement.Files;

namespace Xhznl.FileManagement.EntityFrameworkCore
{
    [DependsOn(
        typeof(FileManagementDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class FileManagementEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<FileManagementDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
                options.AddRepository<File, EfCoreFileRepository>();
            });
        }
    }
}