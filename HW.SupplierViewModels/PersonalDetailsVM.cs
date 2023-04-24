using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
   public class PersonalDetailsVM
    {
        public long SupplierId { get; set; }
        public string UserId { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cnic { get; set; }
        public byte Gender { get; set; }
        public DateTime Dob { get; set; }
        public byte[] ProfileImage { get; set; }
    }
}
