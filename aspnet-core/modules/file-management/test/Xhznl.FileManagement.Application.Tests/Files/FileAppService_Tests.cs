using System.IO;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Shouldly;
using Xunit;

namespace Xhznl.FileManagement.Files
{
    public class FileAppService_Tests : FileManagementApplicationTestBase
    {
        private readonly IFileAppService _fileAppService;

        public FileAppService_Tests()
        {
            _fileAppService = GetRequiredService<IFileAppService>();
        }

        [Fact]
        public async Task GetAsync()
        {
            var result = await _fileAppService.GetAsync("de68532388754ffc84fcbd817397461c.jpg");
            result.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task CreateAsync()
        {
            var result = await _fileAppService.CreateAsync(new FileUploadInputDto()
            {
                Name = "微信图片_20200813165555.jpg",
                Bytes = await File.ReadAllBytesAsync(@"D:\WorkSpace\WorkFiles\杂项\图片\微信图片_20200813165555.jpg")
            });
            result.ShouldNotBeEmpty();
        }
    }
}
