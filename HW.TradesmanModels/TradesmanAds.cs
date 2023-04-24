using System;
using System.Collections.Generic;

namespace HW.TradesmanModels
{
    public partial class TradesmanAds
    {
        public long TradesmanAdsId { get; set; }
        public long TradesmanId { get; set; }
        public int Priority { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime ActiveTo { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
