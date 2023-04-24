using System;
using System.Collections.Generic;

#nullable disable

namespace HW.PackagesAndPaymentsModels
{
    public partial class Redemption
    {
        public long RedemptionsId { get; set; }
        public long VoucherId { get; set; }
        public string RedeemBy { get; set; }
        public DateTime RedeemOn { get; set; }
        public decimal TotalDiscount { get; set; }
        public long JobQuotationId { get; set; }
    }
}
