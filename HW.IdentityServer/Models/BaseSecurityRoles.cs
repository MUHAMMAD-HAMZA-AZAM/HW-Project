using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.IdentityServer.Models
{
    public partial class BaseSecurityRoles
    {
        public int Id { get; set; }
        public string SecurityRoleItem { get; set; }
    }
}
