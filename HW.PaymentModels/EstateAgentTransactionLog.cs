using System;
using System.Collections.Generic;

namespace HW.PaymentModels
{
    public partial class EstateAgentTransactionLog
    {
        public long EstateAgentTransactionLogId { get; set; }
        public long EstateAgentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
