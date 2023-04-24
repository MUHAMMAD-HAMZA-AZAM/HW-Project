using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
  public class GetPaymentRecordVM
    {
        public decimal Amount { get; set; }
        public bool DirectPayment { get; set; }
        public DateTime CreatedOn { get; set; }

        public string Title { get; set; }
        public long JobDetailId { get; set; }

        public string TransactionType { get; set; }
        public decimal? PayableAmount { get; set; }
        public decimal? DiscountedAmount { get; set; }
        public int? PaymentStatus { get; set; }
        public decimal? PaidViaWallet { get; set; }

    }
}
