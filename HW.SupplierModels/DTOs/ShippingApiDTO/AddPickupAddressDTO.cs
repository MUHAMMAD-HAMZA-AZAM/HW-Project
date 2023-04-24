using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierModels.DTOs
{
   public class AddPickupAddressDTO
    {
        public string person_of_contact { get; set; }
        public string phone_number { get; set; }
        public string email_address { get; set; }
        public string address { get; set; }
        public long city_id { get; set; }
    }
}
