using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.IdentityServer.Models
{
    public partial class SecurityRoleDetails
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public bool AllowView { get; set; }
        public bool AllowEdit { get; set; }
        public bool AllowDlete { get; set; }
        public bool AllowPrint { get; set; }
        public bool AllowExport { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}
