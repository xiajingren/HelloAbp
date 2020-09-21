using Xhznl.DataDictionary.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Xhznl.DataDictionary
{
    public abstract class DataDictionaryController : AbpController
    {
        protected DataDictionaryController()
        {
            LocalizationResource = typeof(DataDictionaryResource);
        }
    }
}
