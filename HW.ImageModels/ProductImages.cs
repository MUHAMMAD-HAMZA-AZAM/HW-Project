using System;
using System.Collections.Generic;

namespace HW.ImageModels
{
    public partial class ProductImages
    {
        public long FileId { get; set; }
        public long? ProductId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsMain { get; set; }
    }
}
