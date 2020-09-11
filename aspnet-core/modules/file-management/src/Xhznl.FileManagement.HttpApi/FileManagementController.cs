using Xhznl.FileManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Xhznl.FileManagement
{
    public abstract class FileManagementController : AbpController
    {
        protected FileManagementController()
        {
            LocalizationResource = typeof(FileManagementResource);
        }
    }
}
