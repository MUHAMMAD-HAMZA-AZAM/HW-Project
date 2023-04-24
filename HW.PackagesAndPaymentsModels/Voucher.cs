using System;
using System.Collections.Generic;

namespace HW.PackagesAndPaymentsModels
{
    public partial class Voucher
    {
        public long VoucherId { get; set; }
        public string VoucherCode { get; set; }
        public decimal VoucherDiscount { get; set; }
        public int VoucherDiscountType { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidUntil { get; set; }
        public int RedemptionLimit { get; set; }
    }
}
