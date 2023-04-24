using System;
using System.Collections.Generic;

namespace HW.PaymentModels
{
    public partial class PaymentPlan
    {
        public long PaymentPlanId { get; set; }
        public long RoleId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
