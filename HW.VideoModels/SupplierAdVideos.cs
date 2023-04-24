using System;
using System.Collections.Generic;

namespace HW.VideoModels
{
    public partial class SupplierAdVideos
    {
        public long AdVideoId { get; set; }
        public long SupplierAdsId { get; set; }
        public string VideoName { get; set; }
        public byte[] AdVideo { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
