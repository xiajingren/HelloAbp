using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Xhznl.FileManagement.EntityFrameworkCore;

namespace Xhznl.FileManagement.Files
{
    public class EfCoreFileRepository : EfCoreRepository<IFileManagementDbContext, File, Guid>, IFileRepository
    {
        public EfCoreFileRepository(IDbContextProvider<IFileManagementDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<File> FindByBlobNameAsync(string blobName)
        {
            Check.NotNullOrWhiteSpace(blobName, nameof(blobName));
            var dbset = await GetDbSetAsync();
            return await dbset.FirstOrDefaultAsync(p => p.BlobName == blobName);
        }
    }
}