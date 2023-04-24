using System;
using System.Collections.Generic;

namespace HW.SupplierModels
{
    public partial class OrderCancellationRequest
    {
        public long Id { get; set; }
        public int? CancellationReasonId { get; set; }
        public string Comments { get; set; }
        public bool? CancellationPolicy { get; set; }
        public long? OrderId { get; set; }
        public int? OrderStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UserRole { get; set; }
    }
}
