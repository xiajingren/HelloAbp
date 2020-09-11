using System;
using System.Collections.Generic;
using System.Text;

namespace Xhznl.FileManagement.Files
{
    public class FileOptions
    {
        /// <summary>
        /// 文件上传目录
        /// </summary>
        public string FileUploadLocalFolder { get; set; }

        /// <summary>
        /// 允许的文件最大大小
        /// </summary>
        public long MaxFileSize { get; set; } = 1048576;//1MB

        /// <summary>
        /// 允许的文件类型
        /// </summary>
        public string[] AllowedUploadFormats { get; set; } = { ".jpg", ".jpeg", ".png", "gif", ".txt" };
    }
}
