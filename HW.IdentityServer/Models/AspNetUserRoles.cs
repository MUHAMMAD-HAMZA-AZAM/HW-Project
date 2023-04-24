using System;
using System.Collections.Generic;

namespace HW.IdentityServerModels
{
    public partial class AspNetUserRoles
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public int SellerType { get; set; }
        public long SecurityRole { get; set; }

        public virtual AspNetRoles Role { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
