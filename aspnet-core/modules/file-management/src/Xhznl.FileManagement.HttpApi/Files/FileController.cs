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
        protected IFileAppService FileAppService { get; }

        public FileController(IFileAppService fileAppService)
        {
            FileAppService = fileAppService;
        }

        [HttpGet]
        [Route("{name}")]
        public virtual async Task<FileResult> GetAsync(string name)
        {
            var bytes = await FileAppService.GetAsync(name);
            return File(bytes, MimeTypes.GetByExtension(Path.GetExtension(name)));
        }

        [HttpPost]
        [Route("upload")]
        [Authorize]
        public virtual async Task<JsonResult> CreateAsync(IFormFile file)
        {
            if (file == null)
            {
                throw new UserFriendlyException("No file found!");
            }

            var bytes = await file.GetAllBytesAsync();
            var result = await FileAppService.CreateAsync(new FileUploadInputDto()
            {
                Bytes = bytes,
                Name = file.FileName
            });
            return Json(result);
        }

    }
}