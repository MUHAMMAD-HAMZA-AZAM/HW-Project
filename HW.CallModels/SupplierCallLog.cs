using System;
using System.Collections.Generic;

namespace HW.CallModels
{
    public partial class SupplierCallLog
    {
        public long SupplierCallLogId { get; set; }
        public long CustomerId { get; set; }
        public long SupplierId { get; set; }
        public int? Duration { get; set; }
        public int? CallType { get; set; }
        public string FromCode { get; set; }
        public string ToCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
