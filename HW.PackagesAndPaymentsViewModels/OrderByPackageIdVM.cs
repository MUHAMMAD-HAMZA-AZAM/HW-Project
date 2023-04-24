using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsViewModels
{
   public class OrderByPackageIdVM
    {
        public string UserId { get; set; }
        public List<long> PackageIds { get; set; }
        public long RoleId { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
