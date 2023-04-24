using System;
using System.Collections.Generic;
using System.Text;

namespace HW.IdentityViewModels
{
   public class InactiveUserVM
    {
        public string UserId { get; set; }
        public DateTime ?LastActive { get; set; }
    }
}
