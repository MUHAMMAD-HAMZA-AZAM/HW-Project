using System;
using System.Collections.Generic;

namespace HW.PromotionsModels
{
    public partial class Referral
    {
        public long ReferralId { get; set; }
        public int Role { get; set; }
        public DateTime StartingFrom { get; set; }
        public DateTime EndedAt { get; set; }
        public int Limit { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreateOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
