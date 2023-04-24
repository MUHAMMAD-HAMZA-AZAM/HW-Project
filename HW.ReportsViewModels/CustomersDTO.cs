using System;
using System.Collections.Generic;
using System.Text;

namespace HW.ReportsViewModels
{
  public  class CustomersDTO
    {
        public long CustomerId { get; set; }
        public string UserId { get; set; }
        public string Id { get; set; }
        public string Email { get; set; }
        public string PublicId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cnic { get; set; }
        public byte? Gender { get; set; }
        public DateTime? Dob { get; set; }
        public bool isTestUser { get; set; }
        public string MobileNumber { get; set; }
        public string SourceOfReg { get; set; }
        public string UserType { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string EmailAddress { get; set; }
        public string StreetAddress { get; set; }
        public long? CityId { get; set; }
        public string CityName { get; set; }
        public string LatLong { get; set; }
        public string State { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public DateTime LastActive { get; set; }

    }
}
