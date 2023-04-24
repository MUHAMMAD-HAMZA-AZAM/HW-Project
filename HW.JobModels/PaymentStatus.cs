using System;
using System.Collections.Generic;

namespace HW.JobModels
{
    public partial class PaymentStatus
    {
        public int PaymentStatusId { get; set; }
        public string MethodName { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
