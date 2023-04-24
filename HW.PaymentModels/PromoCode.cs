using System;
using System.Collections.Generic;

namespace HW.PaymentModels
{
    public partial class PromoCode
    {
        public long PromoCodeId { get; set; }
        public string Code { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime ActiveTo { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
