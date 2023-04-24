using System;
using System.Collections.Generic;

namespace HW.PackagesAndPaymentsModels
{
    public partial class AccountReceivablePaymentSummary
    {
        public long PaymentSummaryId { get; set; }
        public long? CustomerId { get; set; }
        public long? OrderId { get; set; }
        public decimal? TotalItemPayment { get; set; }
        public decimal? TotalDeliveryPayment { get; set; }
        public decimal? TotalPayment { get; set; }
        public bool? IsPaymentReceived { get; set; }
    }
}
