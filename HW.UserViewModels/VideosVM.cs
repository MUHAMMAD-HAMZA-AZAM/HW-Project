using System;
using System.Collections.Generic;

namespace HW.UserViewModels
{
    public class VideoVM
    {
        public long VideoId { get; set; }
        public string FilePath { get; set; }
        public byte[] VideoContent { get; set; }
        public long JobQuotationId { get; set; }
        public bool IsActive { get; set; }
    }
}
