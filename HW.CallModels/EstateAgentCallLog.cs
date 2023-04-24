using System;
using System.Collections.Generic;

namespace HW.CallModels
{
    public partial class EstateAgentCallLog
    {
        public long EstateAgentCallLogId { get; set; }
        public long CustomerId { get; set; }
        public long EstateAgentId { get; set; }
        public int? Duration { get; set; }
        public string FromCode { get; set; }
        public string ToCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
