using System;
using System.Collections.Generic;

namespace HW.ImageModels
{
    public partial class JobImages
    {
        public long BidImageId { get; set; }
        public string FileName { get; set; }
        public byte[] BidImage { get; set; }
        public bool IsMain { get; set; }
        public long JobQuotationId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
