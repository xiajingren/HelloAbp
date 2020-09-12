using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.Settings;
using Volo.Abp.Validation;
using Xhznl.FileManagement.Localization;
using Xhznl.FileManagement.Settings;

namespace Xhznl.FileManagement.Files
{
    public class FileAppService : FileManagementAppService, IFileAppService
    {
        private readonly FileOptions _fileOptions;
        private readonly ISettingProvider _settingProvider;
        private readonly IStringLocalizer<FileManagementResource> _localizer;

        public FileAppService(IOptions<FileOptions> fileOptions, ISettingProvider settingProvider, IStringLocalizer<FileManagementResource> localizer)
        {
            _fileOptions = fileOptions.Value;
            _settingProvider = settingProvider;
            _localizer = localizer;
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
        public async Task<string> CreateAsync(FileUploadInputDto input)
        {
            if (input.Bytes.IsNullOrEmpty())
            {
                throw new AbpValidationException("Bytes can not be null or empty!",
                    new List<ValidationResult>
                    {
                        new ValidationResult("Bytes can not be null or empty!", new[] {"Bytes"})
                    });
            }

            var allowedMaxFileSize = await _settingProvider.GetAsync<int>(FileManagementSettings.AllowedMaxFileSize);//kb
            var allowedUploadFormats = (await _settingProvider.GetOrNullAsync(FileManagementSettings.AllowedUploadFormats))
                ?.Split(",", StringSplitOptions.RemoveEmptyEntries);

            if (input.Bytes.Length > allowedMaxFileSize * 1024)
            {
                throw new UserFriendlyException(_localizer["FileManagement.ExceedsTheMaximumSize", allowedMaxFileSize]);
            }

            if (allowedUploadFormats == null || !allowedUploadFormats.Contains(Path.GetExtension(input.Name)))
            {
                throw new UserFriendlyException(_localizer["FileManagement.NotValidFormat"]);
            }

            var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(input.Name);
            var filePath = Path.Combine(_fileOptions.FileUploadLocalFolder, fileName);

            if (!Directory.Exists(_fileOptions.FileUploadLocalFolder))
            {
                Directory.CreateDirectory(_fileOptions.FileUploadLocalFolder);
            }

            File.WriteAllBytes(filePath, input.Bytes);

            return "/api/file-management/files/" + fileName;
        }
    }
}