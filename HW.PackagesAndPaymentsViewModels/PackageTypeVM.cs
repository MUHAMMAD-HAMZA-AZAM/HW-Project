using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsViewModels
{
   public class PackageTypeVM
    {
        public long? PackageTypeId { get; set; }
        public string PackageTypeName { get; set; }
        public string PackageTypeCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }    
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
        public string EntityStatus { get; set; }
        public int UserRoleId { get; set; }



    }
}

