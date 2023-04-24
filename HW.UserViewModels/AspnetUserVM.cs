using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
   public  class AspnetUserVM
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhonenNumber { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastActive { get; set; }
        public string Role { get; set; }
    }
}
