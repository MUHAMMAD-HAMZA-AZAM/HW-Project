using System;
using System.Collections.Generic;

namespace HW.EstateAgentModels
{
    public partial class EstateAgentAds
    {
        public long EstateAgentAdsId { get; set; }
        public long EsateAgentId { get; set; }
        public int Priority { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime ActiveTo { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
