using System;
using System.Collections.Generic;
using System.Text;

namespace HW.IdentityViewModels
{
    public class DeleteUserInfoVM
    {
      public string UserName { get; set; }
      public string Phone { get; set; }
      public string UserRole { get; set; }
      public string UserRegistrationType { get; set; }
      public string userId { get; set; }
      public string deletedBy { get; set; }
    }
}
