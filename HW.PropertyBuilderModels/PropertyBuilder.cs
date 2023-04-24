using System;
using System.Collections.Generic;

namespace HW.PropertyBuilderModels
{
    public partial class PropertyBuilder
    {
        public long PropertyBuilderId { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cnic { get; set; }
        public byte Gender { get; set; }
        public DateTime Dob { get; set; }
        public string MobileNumber { get; set; }
        public string PrimaryTrade { get; set; }
        public string CompanyName { get; set; }
        public long? RegisterationNo { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string StreetAddress { get; set; }
        public string EmailAddress { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
