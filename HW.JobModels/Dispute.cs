using System;
using System.Collections.Generic;

namespace HW.JobModels
{
    public partial class Dispute
    {
        public long DisputeId { get; set; }
        public long CustomerId { get; set; }
        public int JobStatusId { get; set; }
        public long JobDetailId { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public long DisputeStatusId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
