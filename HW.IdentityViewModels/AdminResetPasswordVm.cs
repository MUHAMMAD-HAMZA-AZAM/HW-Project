using System;
using System.Collections.Generic;
using System.Text;

namespace HW.IdentityViewModels
{
    public class AdminResetPasswordVm
    {
        public string UserId { get; set; }
        public string Passwords { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
