using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.Validation;

namespace Xhznl.FileManagement.Files
{
    public class FileAppService : FileManagementAppService, IFileAppService
    {
        private readonly FileOptions _fileOptions;

        public FileAppService(IOptions<FileOptions> fileOptions)
        {
            _fileOptions = fileOptions.Value;
        }

        public Task<byte[]> GetAsync(string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var filePath = Path.Combine(_fileOptions.FileUploadLocalFolder, name);

            if (File.Exists(filePath))
            {
                return Task.FromResult(File.ReadAllBytes(filePath));
            }

            return Task.FromResult(new byte[0]);
        }

        [Authorize]
        public Task<string> CreateAsync(FileUploadInputDto input)
        {
            if (input.Bytes.IsNullOrEmpty())
            {
                throw new AbpValidationException("Bytes can not be null or empty!",
                    new List<ValidationResult>
                    {
                        new ValidationResult("Bytes can not be null or empty!", new[] {"Bytes"})
                    });
            }

            if (input.Bytes.Length > _fileOptions.MaxFileSize)
            {
                throw new UserFriendlyException($"File exceeds the maximum upload size ({_fileOptions.MaxFileSize / 1024 / 1024} MB)!");
            }

            if (!_fileOptions.AllowedUploadFormats.Contains(Path.GetExtension(input.Name)))
            {
                throw new UserFriendlyException("Not a valid file format!");
            }

            var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(input.Name);
            var filePath = Path.Combine(_fileOptions.FileUploadLocalFolder, fileName);

            if (!Directory.Exists(_fileOptions.FileUploadLocalFolder))
            {
                Directory.CreateDirectory(_fileOptions.FileUploadLocalFolder);
            }

            File.WriteAllBytes(filePath, input.Bytes);

            return Task.FromResult("/api/file-management/files/" + fileName);
        }
    }
}