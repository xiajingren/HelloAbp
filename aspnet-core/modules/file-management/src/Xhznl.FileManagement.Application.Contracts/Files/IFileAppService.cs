using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Xhznl.FileManagement.Files
{
    public interface IFileAppService : IApplicationService
    {
        Task<FileDto> FindByBlobNameAsync(string blobName);

        Task<string> CreateAsync(FileDto input);
    }
}
