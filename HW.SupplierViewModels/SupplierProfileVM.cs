using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
    public class SupplierProfileVM
    {
        public string FullName { get; set; }
        public byte[] ProfileImage { get; set; }
        public string CompanyName { get; set; }
        public string FirebaseClientId { get; set; }
        public bool? PhoneNumberConfirmed { get; set; }
        public string PhoneNumber { get; set; }
    }
}
