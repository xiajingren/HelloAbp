using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Xhznl.FileManagement.Files
{
    public interface IFileManager : IDomainService
    {
        Task<File> FindByBlobNameAsync(string blobName);

        Task<File> CreateAsync(string fileName, byte[] bytes);

        Task<byte[]> GetBlobAsync(string blobName);
    }
}