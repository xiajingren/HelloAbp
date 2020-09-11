using Xhznl.FileManagement.Localization;
using Volo.Abp.Application.Services;

namespace Xhznl.FileManagement
{
    public abstract class FileManagementAppService : ApplicationService
    {
        protected FileManagementAppService()
        {
            LocalizationResource = typeof(FileManagementResource);
            ObjectMapperContext = typeof(FileManagementApplicationModule);
        }
    }
}
