using System;
using System.Collections.Generic;

namespace HW.PackagesAndPaymentsModels
{
    public partial class ReferalCode
    {
        public long ReferralId { get; set; }
        public string ReferralCode { get; set; }
        public string UserId { get; set; }
        public string ReferredUser { get; set; }
        public long JobQuotationId { get; set; }
        public bool IsJobComplete { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public decimal? RefferalAmount { get; set; }
    }
}
