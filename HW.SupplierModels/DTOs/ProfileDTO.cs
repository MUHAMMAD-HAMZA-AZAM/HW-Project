using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierModels.DTOs
{
    public class ProfileDTO
    {
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
        public long? ProductCategoryId { get; set; }
        public string State { get; set; }
        public int? StateId { get; set; }
        public long? CityId { get; set; }
        public string BusinessAddress { get; set; }
        public string GpsCoordinates { get; set; }
        public int? DeliveryRadius { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public bool? IsActiv { get; set; }
        public long SupplierId { get; set; }
        public bool? FeaturedSupplier { get; set; }
        public int? SupplierRole { get; set; }
        public string ShopName { get; set; }
        public string ShopUrl { get; set; }
        public bool? HolidayMode { get; set; }
        public DateTime? HolidayStart { get; set; }
        public DateTime? HoilidayEnd { get; set; }
        public int? CountryId { get; set; }
        public string idFrontImage { get; set; }
        public string idBackSideImage { get; set; }
        public byte[] FrontImage { get; set; }
        public byte[] BackImage { get; set; }
        public string ShopCoverImage { get; set; }
        public string BusinessDescription { get;set;}
        public long? AreaId { get; set; }
        public string Location { get; set; }
        public string InChargePerson { get; set; }
        public string InchargePersonMobileNo { get; set; }
        public string InchargePersonEmail { get; set; }
        public string TabName { get; set; }
        public string Ntnnumber { get; set; }

    }
}
