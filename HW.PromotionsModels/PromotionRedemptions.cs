using System;
using System.Collections.Generic;

namespace HW.PromotionsModels
{
    public partial class PromotionRedemptions
    {
        public long PromotionRedemptionsId { get; set; }
        public long PromotionId { get; set; }
        public string RedeemBy { get; set; }
        public DateTime RedeemOn { get; set; }
        public decimal TotalDiscount { get; set; }
        public long JobQuotationId { get; set; }
    }
}
