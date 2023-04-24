using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HW.UserViewModels
{
    public class ImageVM
    {
        public long Id { get; set; }
        public string FilePath { get; set; }
        [NotMapped]
        public string ImageBase64 { get; set; }
        public bool IsMain { get; set; }
        public byte[] ImageContent { get; set; }
        [NotMapped]
        public string LocalUrl { get; set; }
    }
}
