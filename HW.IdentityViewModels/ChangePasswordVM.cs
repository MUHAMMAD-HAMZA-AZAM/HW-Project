using System;
using System.Collections.Generic;
using System.Text;

namespace HW.IdentityViewModels
{
    public class ChangePasswordVM
    {        
        public string adminId { get; set; }
        public string currentPassword { get; set; }
        public string newPassword { get; set; }
    }
}
