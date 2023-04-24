using System;
using System.Collections.Generic;

namespace HW.PackagesAndPaymentsModels
{
    public partial class PackageType
    {
        public long PackageTypeId { get; set; }
        public string PackageTypeName { get; set; }
        public string PackageTypeCode { get; set; }
        public int? RoleId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
        public string EntityStatus { get; set; }
    }
}
