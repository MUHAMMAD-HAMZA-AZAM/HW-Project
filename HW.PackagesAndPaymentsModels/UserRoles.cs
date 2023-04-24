using System;
using System.Collections.Generic;

namespace HW.PackagesAndPaymentsModels
{
    public partial class UserRoles
    {
        public long UserRoleId { get; set; }
        public string UserRoleCode { get; set; }
        public string UserRoleName { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
