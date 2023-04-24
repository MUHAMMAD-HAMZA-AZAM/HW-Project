using System;
using System.Collections.Generic;

namespace HW.JobModels
{
    public partial class JobFeedback
    {
        public long JobFeedbackId { get; set; }
        public long JobDetailId { get; set; }
        public long CustomerId { get; set; }
        public long? TradesmanId { get; set; }
        public long? EstateAgentId { get; set; }
        public long? FromId { get; set; }
        public long? ToId { get; set; }
        public string FromCode { get; set; }
        public string ToCode { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
