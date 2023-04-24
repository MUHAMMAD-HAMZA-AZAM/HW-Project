using System;
using System.Collections.Generic;

namespace HW.CustomerModels
{
    public partial class Customer
    {
        public string UserId { get; set; }
        public string PublicId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cnic { get; set; }
        public byte? Gender { get; set; }
        public DateTime? Dob { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public string StreetAddress { get; set; }
        public long? CityId { get; set; }
        public string LatLong { get; set; }
        public string State { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public long CustomerId { get; set; }
        public long? RegisteredBy { get; set; }
    }
}
