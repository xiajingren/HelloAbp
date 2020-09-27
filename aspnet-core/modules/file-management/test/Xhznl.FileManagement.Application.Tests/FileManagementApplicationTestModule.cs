using Volo.Abp.BlobStoring;
using Volo.Abp.Modularity;
using Xhznl.FileManagement.Files;
using Volo.Abp.BlobStoring.FileSystem;

namespace Xhznl.FileManagement
{
    [DependsOn(
        typeof(FileManagementApplicationModule),
        typeof(FileManagementDomainTestModule)
        )]
    [DependsOn(typeof(AbpBlobStoringFileSystemModule))]
    public class FileManagementApplicationTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {

            Configure<AbpBlobStoringOptions>(options =>
            {
                options.Containers.ConfigureDefault(container =>
                {
                    container.UseFileSystem(fileSystem =>
                    {
                        fileSystem.BasePath = "D:\\my-files";
                    });
                });
            });

            base.ConfigureServices(context);
        }
    }
}
