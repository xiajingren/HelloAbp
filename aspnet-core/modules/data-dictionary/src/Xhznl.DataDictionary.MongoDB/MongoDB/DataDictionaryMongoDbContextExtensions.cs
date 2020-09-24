using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Xhznl.DataDictionary.MongoDB
{
    public static class DataDictionaryMongoDbContextExtensions
    {
        public static void ConfigureDataDictionary(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new DataDictionaryMongoModelBuilderConfigurationOptions(
                DataDictionaryDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}