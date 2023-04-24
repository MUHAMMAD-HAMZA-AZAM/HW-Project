using System;
using System.Collections.Generic;

namespace HW.SupplierModels
{
    public partial class SupplierAdImage
    {
        public long AdImageId { get; set; }
        public long SupplierAdsId { get; set; }
        public string FileName { get; set; }
        public bool? IsMain { get; set; }
        public byte[] AdImage { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
