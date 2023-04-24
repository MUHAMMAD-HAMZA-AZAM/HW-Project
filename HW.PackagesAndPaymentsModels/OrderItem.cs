using System;
using System.Collections.Generic;

namespace HW.PackagesAndPaymentsModels
{
    public partial class OrderItem
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public long PackageId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
