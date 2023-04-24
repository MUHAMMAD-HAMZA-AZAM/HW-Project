using System;
using System.Collections.Generic;

namespace HW.PaymentModels
{
    public partial class TradesmanTransactionLog
    {
        public long TradesmanTransactionLogId { get; set; }
        public long TradesmanId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
