using System;
using System.Collections.Generic;

namespace HW.PackagesAndPaymentsModels
{
    public partial class ReferralDetails
    {
        public long ReferralDetailId { get; set; }
        public long ReferralId { get; set; }
        public long ReferralCodeId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
