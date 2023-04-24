using System;
using System.Collections.Generic;

namespace HW.OrganizationModels
{
    public partial class Organization
    {
        public long OrganizationId { get; set; }
        public string UserId { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cnic { get; set; }
        public byte Gender { get; set; }
        public DateTime Dob { get; set; }
        public string CompanyName { get; set; }
        public long? RegistrationNo { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string StreetAddress { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
