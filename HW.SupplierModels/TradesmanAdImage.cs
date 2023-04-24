using System;
using System.Collections.Generic;

namespace HW.SupplierModels
{
    public partial class TradesmanAdImage
    {
        public long AdImageId { get; set; }
        public long TradesmanAdsId { get; set; }
        public byte[] AdImage { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
