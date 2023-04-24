using System;
using System.Collections.Generic;

namespace HW.PackagesAndPaymentsModels
{
    public partial class AccountReceivablePaymentDetail
    {
        public long PaymentDetailId { get; set; }
        public long? PaymentId { get; set; }
        public long? SupplierId { get; set; }
        public long? OrderDetailId { get; set; }
        public long? ItemId { get; set; }
        public decimal? ItemCost { get; set; }
        public decimal? ItemDeliveryCost { get; set; }
        public decimal? TotalItemCost { get; set; }
        public int? PaymentStatus { get; set; }
        public bool? PaymentReceivedStatus { get; set; }
        public bool? DispatchPaymentStatus { get; set; }
    }
}
