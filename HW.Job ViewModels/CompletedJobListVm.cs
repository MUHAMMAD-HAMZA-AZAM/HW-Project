using System;
using System.Collections.Generic;
using System.Text;

namespace HW.Job_ViewModels
{
    public class CompletedJobListVm
    {
        public long jobId { get; set; }
        public long customerId { get; set; }
        public string jobTitle { get; set; }
        public long JobDetailId { get; set; }
        public long JobQuotationId { get; set; }
        public string City { get; set; }
        public DateTime StartDate { get; set; }
        public string MobileNumber { get; set; }
        public int ?OverallRating { get; set; }
        public string Comments { get; set; }
        public string customerName { get; set; }
        public long NoOfRecoards { get; set; }
        public long RecordNo { get; set; }
    }
}
