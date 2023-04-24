using System;
using System.Collections.Generic;

namespace HW.SupplierModels
{
    public partial class OrderCancellationReason
    {
        public int Id { get; set; }
        public string ReasonName { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public int? UserRole { get; set; }
    }
}
