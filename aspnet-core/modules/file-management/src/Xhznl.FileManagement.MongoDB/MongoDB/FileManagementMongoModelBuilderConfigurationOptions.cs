using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace Xhznl.FileManagement.MongoDB
{
    public class FileManagementMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public FileManagementMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}