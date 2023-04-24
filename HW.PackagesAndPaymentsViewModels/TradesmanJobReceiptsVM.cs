using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsViewModels
{
    public class TradesmanJobReceiptsVM
    {
        public long TradesmanJobReceiptsId { get; set; }
        public long JobQuotationId { get; set; }
        public long JobDetailId { get; set; }
        public long TradesmanId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public decimal TradesmanInitialBudget { get; set; }
        public bool DirectPayment { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public int PaymentMethodId { get; set; }
        public long? TransactionDetailId { get; set; }
        public decimal? PayableAmount { get; set; }
        public long? CustomerId { get; set; }
        public decimal? DiscountedAmount { get; set; }
        public decimal? ServiceCharges { get; set; }
        public decimal? OtherCharges { get; set; }
        public decimal? Commission { get; set; }
        public decimal? NetPayableToTradesman { get; set; }
        public int? PaymentStatus { get; set; }
        public decimal? PaidViaWallet { get; set; }
        public decimal? AdditionalCharges { get; set; }
    }
}
