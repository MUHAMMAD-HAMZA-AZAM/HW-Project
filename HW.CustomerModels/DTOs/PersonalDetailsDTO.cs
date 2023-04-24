using System;
using System.Collections.Generic;
using System.Text;

namespace HW.CustomerModels.DTOs
{
    public class PersonalDetailsDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Cnic { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Role { get; set; }
        public string MobileNumber { get; set; }
        public string City { get; set; }
        public long? CityId { get; set; }
        public string Address { get; set; }
        public long? CustomerId { get; set; }

    }
}
