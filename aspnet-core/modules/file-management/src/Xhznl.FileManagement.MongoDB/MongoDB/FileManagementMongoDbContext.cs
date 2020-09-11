using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Xhznl.FileManagement.MongoDB
{
    [ConnectionStringName(FileManagementDbProperties.ConnectionStringName)]
    public class FileManagementMongoDbContext : AbpMongoDbContext, IFileManagementMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureFileManagement();
        }
    }
}