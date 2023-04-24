using System;
using System.Collections.Generic;

namespace HW.SupplierModels
{
    public partial class DisputeImages
    {
        public long DisputeImageId { get; set; }
        public string FileName { get; set; }
        public byte[] BidImage { get; set; }
        public bool IsMain { get; set; }
        public long DisputeId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
