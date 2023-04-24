using System;
using System.Collections.Generic;

namespace HW.PaymentModels
{
    public partial class AdvertismentPlan
    {
        public long AdvertisementPlanId { get; set; }
        public decimal Price { get; set; }
        public int Days { get; set; }
        public long AdsStatusId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
