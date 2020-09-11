using Volo.Abp.Modularity;
using Xhznl.FileManagement.Files;

namespace Xhznl.FileManagement
{
    [DependsOn(
        typeof(FileManagementApplicationModule),
        typeof(FileManagementDomainTestModule)
        )]
    public class FileManagementApplicationTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<FileOptions>(options =>
            {
                options.FileUploadLocalFolder = "D:\\my-files";
            });
            base.ConfigureServices(context);
        }
    }
}
