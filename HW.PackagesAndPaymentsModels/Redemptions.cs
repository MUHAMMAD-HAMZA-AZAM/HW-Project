using System;
using System.Collections.Generic;

namespace HW.PackagesAndPaymentsModels
{
    public partial class Redemptions
    {
        public long RedemptionsId { get; set; }
        public long VoucherId { get; set; }
        public string RedeemBy { get; set; }
        public DateTime RedeemOn { get; set; }
        public decimal TotalDiscount { get; set; }
        public long JobQuotationId { get; set; }
    }
}
