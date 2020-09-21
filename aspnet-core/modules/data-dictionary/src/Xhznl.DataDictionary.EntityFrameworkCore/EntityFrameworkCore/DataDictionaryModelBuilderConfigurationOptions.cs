using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Xhznl.DataDictionary.EntityFrameworkCore
{
    public class DataDictionaryModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public DataDictionaryModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}