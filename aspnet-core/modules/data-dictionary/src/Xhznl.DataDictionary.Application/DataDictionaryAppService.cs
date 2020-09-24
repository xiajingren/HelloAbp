using Xhznl.DataDictionary.Localization;
using Volo.Abp.Application.Services;

namespace Xhznl.DataDictionary
{
    public abstract class DataDictionaryAppService : ApplicationService
    {
        protected DataDictionaryAppService()
        {
            LocalizationResource = typeof(DataDictionaryResource);
            ObjectMapperContext = typeof(DataDictionaryApplicationModule);
        }
    }
}
