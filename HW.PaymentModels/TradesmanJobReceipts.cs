using System;
using System.Collections.Generic;

namespace HW.PaymentModels
{
    public partial class TradesmanJobReceipts
    {
        public long TradesmanJobReceiptsId { get; set; }
        public long JobDetailId { get; set; }
        public long TradesmanId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public bool DirectPayment { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public int PaymentMethodId { get; set; }
        public long? TransactionDetailId { get; set; }
        public decimal? PayableAmount { get; set; }
    }
}
