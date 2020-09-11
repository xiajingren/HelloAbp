using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Xhznl.FileManagement
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(FileManagementDomainSharedModule)
    )]
    public class FileManagementDomainModule : AbpModule
    {

    }
}
