using System;
using System.Collections.Generic;

namespace HW.PackagesAndPaymentsModels
{
    public partial class PromotionRedemptions
    {
        public long PromotionRedemptionsId { get; set; }
        public long? PromotionId { get; set; }
        public string RedeemBy { get; set; }
        public DateTime RedeemOn { get; set; }
        public decimal TotalDiscount { get; set; }
        public long JobQuotationId { get; set; }
        public long? CustomerId { get; set; }
        public long? TradesmanId { get; set; }
        public long? SupplierId { get; set; }
        public long? JobDetailId { get; set; }
        public bool? IsVoucher { get; set; }
        public long? VoucherBookLeavesId { get; set; }
        public long? CategoryId { get; set; }
    }
}
