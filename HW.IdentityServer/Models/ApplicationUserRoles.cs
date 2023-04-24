using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HW.IdentityServer.Models
{
    public class ApplicationUserRoles : IdentityRole
    {
        public long SecutityRole { get; set; }
    }
}
