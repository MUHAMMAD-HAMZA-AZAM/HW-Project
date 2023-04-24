using System;
using System.Collections.Generic;

namespace HW.PaymentModels
{
    public partial class SupplierTransactionLog
    {
        public long SupplierTransactionLogId { get; set; }
        public long SupplierId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
