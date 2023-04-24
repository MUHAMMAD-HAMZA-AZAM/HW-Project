using System;
using System.Collections.Generic;

namespace HW.CallModels
{
    public partial class TradesmanCallLog
    {
        public long TradesmanCallLogId { get; set; }
        public long CustomerId { get; set; }
        public long TradesmanId { get; set; }
        public long? JobQuotationId { get; set; }
        public int? Duration { get; set; }
        public int? CustomerCallType { get; set; }
        public int? CallType { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
