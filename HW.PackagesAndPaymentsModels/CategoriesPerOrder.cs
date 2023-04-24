using System;
using System.Collections.Generic;

namespace HW.PackagesAndPaymentsModels
{
    public partial class CategoriesPerOrder
    {
        public long CatagoryPerOrderId { get; set; }
        public long? OrderId { get; set; }
        public string AspnetUserId { get; set; }
        public long? UserRoleId { get; set; }
        public long? CatagoryId { get; set; }
        public string EntityStatus { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
