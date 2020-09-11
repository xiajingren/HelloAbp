using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Http;

namespace Xhznl.FileManagement.Files
{
    [RemoteService]
    [Route("api/file-management/files")]
    public class FileController : FileManagementController
    {
        private readonly IFileAppService _fileAppService;

        public FileController(IFileAppService fileAppService)
        {
            _fileAppService = fileAppService;
        }

        [HttpGet]
        [Route("{name}")]
        public async Task<FileResult> GetAsync(string name)
        {
            var bytes = await _fileAppService.GetAsync(name);
            return File(bytes, MimeTypes.GetByExtension(Path.GetExtension(name)));
        }

        [HttpPost]
        [Route("upload")]
        [Authorize]
        public async Task<JsonResult> CreateAsync(IFormFile file)
        {
            if (file == null)
            {
                throw new UserFriendlyException("No file found!");
            }

            var bytes = await file.GetAllBytesAsync();
            var result = await _fileAppService.CreateAsync(new FileUploadInputDto()
            {
                Bytes = bytes,
                Name = file.FileName
            });
            return Json(result);
        }

    }
}