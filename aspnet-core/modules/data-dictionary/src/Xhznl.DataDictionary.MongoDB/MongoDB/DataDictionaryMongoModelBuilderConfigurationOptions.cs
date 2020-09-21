using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace Xhznl.DataDictionary.MongoDB
{
    public class DataDictionaryMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public DataDictionaryMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}