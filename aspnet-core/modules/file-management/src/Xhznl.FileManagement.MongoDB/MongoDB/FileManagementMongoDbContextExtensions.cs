using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Xhznl.FileManagement.MongoDB
{
    public static class FileManagementMongoDbContextExtensions
    {
        public static void ConfigureFileManagement(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new FileManagementMongoModelBuilderConfigurationOptions(
                FileManagementDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}