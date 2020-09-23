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
        public async Task Create_FindByBlobName_Test()
        {
            var blobName = await _fileAppService.CreateAsync(new FileDto()
            {
                FileName = "微信图片_20200813165555.jpg",
                Bytes = await System.IO.File.ReadAllBytesAsync(@"D:\WorkSpace\WorkFiles\杂项\图片\微信图片_20200813165555.jpg")
            });
            blobName.ShouldNotBeEmpty();

            var fileDto = await _fileAppService.FindByBlobNameAsync(blobName);
            fileDto.ShouldNotBeNull();
            fileDto.FileName.ShouldBe("微信图片_20200813165555.jpg");
        }
    }
}
