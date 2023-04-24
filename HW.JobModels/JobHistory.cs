using System;
using System.Collections.Generic;

namespace HW.JobModels
{
    public partial class JobHistory
    {
        public int JobHistoryId { get; set; }
        public string JobQuotationId { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string DeletedBy { get; set; }
        public string Status { get; set; }
    }
}
