using System;
using System.Collections.Generic;

#nullable disable

namespace HW.TradesmanModels
{
    public partial class Tradesman
    {
        public long TradesmanId { get; set; }
        public string UserId { get; set; }
        public string PublicId { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cnic { get; set; }
        public byte? Gender { get; set; }
        public DateTime? Dob { get; set; }
        public long? TravellingDistance { get; set; }
        public bool? IsFavorite { get; set; }
        public bool? IsOrganization { get; set; }
        public string CompanyName { get; set; }
        public string CompanyRegNo { get; set; }
        public string GpsCoordinates { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string ShopAddress { get; set; }
        public string AddressLine { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
    }
}
