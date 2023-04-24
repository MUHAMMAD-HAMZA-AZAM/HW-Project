using HW.SupplierViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace HW.ReportsViewModels
{
   public class SupplierDTO
    {
        public long SupplierId { get; set; }
        public string UserId { get; set; }
        public string PublicId { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cnic { get; set; }
        public byte? Gender { get; set; }
        public DateTime? Dob { get; set; }
        public string CompanyName { get; set; }
        public string RegistrationNumber { get; set; }
        
        public string PrimaryTrade { get; set; }
        public string SupplierType { get; set; }
        public long? ProductCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string State { get; set; }
        public long? CityId { get; set; }
        public string CityName { get; set; }
        public string Town { get; set; }
        public string BusinessAddress { get; set; }
        public string GpsCoordinates { get; set; }
        public int? DeliveryRadius { get; set; }
        public bool? IsActive { get; set; }
        public bool isTestUser { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool FeaturedSupplier { get; set; }

        public bool EmailConfirmed { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public bool? IsActiv { get; set; }
        public DateTime LastActive { get; set; }
        public long SupplierCount { get; set; }
        public byte[] ProfileImage { get; set; }
        public byte[] ProfileImage1 { get; set; }
        public byte[] ProfileImage2 { get; set; }
        public byte[] ProfileImage3 { get; set; }
        public bool FImageStatus { get; set; }
        public int ImageId1 { get; set; }
        public int ImageId2 { get; set; }
        public int ImageId3 { get; set; }
        public bool ImageStatus1 { get; set; }
        public bool ImageStatus2 { get; set; }
        public bool ImageStatus3 { get; set; }
    }
}
