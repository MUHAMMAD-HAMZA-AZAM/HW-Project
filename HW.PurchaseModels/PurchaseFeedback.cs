using System;
using System.Collections.Generic;

namespace HW.PurchaseModels
{
    public partial class PurchaseFeedback
    {
        public long PurchaseFeedbackId { get; set; }
        public long FromId { get; set; }
        public long ToId { get; set; }
        public long PurchaseDetailId { get; set; }
        public long CustomerId { get; set; }
        public long SupplierId { get; set; }
        public string FromCode { get; set; }
        public string ToCode { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
