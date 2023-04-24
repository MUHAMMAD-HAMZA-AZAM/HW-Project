using System;
using System.Collections.Generic;

namespace HW.ImageModels
{
    public partial class RsaAdImage
    {
        public long AdImageId { get; set; }
        public long EstateAgentAdsId { get; set; }
        public byte[] AdImage { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
