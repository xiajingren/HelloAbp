using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Xhznl.FileManagement.Files
{
    public interface IFileAppService : IApplicationService
    {
        Task<byte[]> GetAsync(string name);

        Task<string> CreateAsync(FileUploadInputDto input);
    }
}
