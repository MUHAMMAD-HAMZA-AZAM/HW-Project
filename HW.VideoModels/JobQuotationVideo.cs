using System;
using System.Collections.Generic;

namespace HW.VideoModels
{
    public partial class JobQuotationVideo
    {
        public long VideoId { get; set; }
        public string FileName { get; set; }
        public byte[] Video { get; set; }
        public long? JobQuotationId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
