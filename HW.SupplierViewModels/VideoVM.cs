using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
    public class VideoVM
    {
        public long VideoId { get; set; }
        public string FilePath { get; set; }
        public byte[] VideoContent { get; set; }
    }
}
