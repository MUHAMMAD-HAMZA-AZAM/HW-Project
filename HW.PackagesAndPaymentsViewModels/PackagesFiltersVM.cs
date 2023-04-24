using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsViewModels
{
   public class PackagesFiltersVM
    {
        public string PackageName { get; set; }
        public string PackageCode { get; set; }
        public string Entity { get; set; }
        public int UserRoleId { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public string OrderByColumn { get; set; }
    }
}
