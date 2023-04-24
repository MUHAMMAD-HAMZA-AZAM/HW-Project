using System;
using System.Collections.Generic;
using System.Text;

namespace HW.TradesmanViewModels
{
    public class InvoiceVM
    {
        public string PaymentMonth { get; set; }
        public string PaymentMode { get; set; }
        public decimal Amount { get; set; }
        public decimal TradesmanInitialBudget { get; set; }
        public int? PaymentStatus { get; set; }
        public decimal? DiscountedAmount { get; set; }
        public decimal? ServiceCharges { get; set; }
        public decimal? OtherCharges { get; set; }
        public decimal? AdditionalCharges { get; set; }
        public decimal? Commission { get; set; }
        public decimal? NetPayableToTradesman { get; set; }
        public decimal? PayableAmount { get; set; }
        public decimal? PaidViaWallet { get; set; }
        public long JobQuotationId { get; set; }
    }
}
