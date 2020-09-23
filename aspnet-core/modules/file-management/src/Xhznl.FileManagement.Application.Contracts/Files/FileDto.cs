using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Xhznl.FileManagement.Files
{
    public class FileDto
    {
        [Required]
        public byte[] Bytes { get; set; }

        [Required]
        public string FileName { get; set; }
    }
}
