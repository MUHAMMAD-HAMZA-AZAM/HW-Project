using System;
using System.Collections.Generic;

namespace HW.PackagesAndPaymentsModels
{
    public partial class AccountReceivablePayment
    {
        public long PaymentId { get; set; }
        public long? DeliveryCompanyId { get; set; }
        public long? OrderId { get; set; }
        public int? PaymentMethodId { get; set; }
        public bool? IsPaymentReceived { get; set; }
        public decimal? TotalOrderCost { get; set; }
        public decimal? TotalDeliveryCost { get; set; }
        public int? PaymentStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
