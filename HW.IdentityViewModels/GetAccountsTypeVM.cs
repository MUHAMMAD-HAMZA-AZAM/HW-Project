using System;
using System.Collections.Generic;
using System.Text;

namespace HW.IdentityViewModels
{
   public class GetAccountsTypeVM
    {
        public string UserId { get; set; }
        public int? SellerType { get; set; }
        public int? AccountType { get; set; }
        public int? Role { get; set; }
    }
}
