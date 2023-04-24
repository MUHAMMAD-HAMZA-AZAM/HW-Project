using System;
using System.Collections.Generic;
using System.Text;

namespace HW.IdentityViewModels
{
   public class GetAdminUserDetails
    {
        public string Id { get; set;}
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedOn { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public DateTime LastActive { get; set; }
        public string Password { get; set; }
        public string SecurityRole { get; set; }
        public string SecurityRoleName { get; set; }
        public bool IsTestUser { get; set; }
    }
}
