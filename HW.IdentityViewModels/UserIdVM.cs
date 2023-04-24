using System;
using System.Collections.Generic;
using System.Text;

namespace HW.IdentityViewModels
{
    public class UserIdVM
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public long? SellerType { get; set; }
        public string FirebaseClientId { get; set; }
    }
}
