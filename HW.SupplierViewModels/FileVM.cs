using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
    public class FileVM
    {
        public long? FileId { get; set; }
        public long? ProductId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public bool? IsMain { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
