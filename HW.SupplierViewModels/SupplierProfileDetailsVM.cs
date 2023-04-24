using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
   public class SupplierProfileDetailHistroryVM
    {
        public string SupplierName { get; set; }
        public string UserId { get; set; }
        public string PublicId { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public string Cnic { get; set; }
        public string CompanyName { get; set; }
        public string RegistrationNumber { get; set; }
        public string StateName { get; set; }
        public string BusinessAddress { get; set; }
        public long SupplierId { get; set; }
        public int? SupplierRole { get; set; }
        public long UnAuthorizedCountSupplierProduct { get; set; }
        public long AuthorizedCountSupplierProduct { get; set; }
        public string ShopName { get; set; }
        public string CountryName { get; set; }
        public byte[] IdfrontImage { get; set; }
        public string IdfrontImageName { get; set; }
        public byte[] IdbackImage { get; set; }
        public byte[] ShopCoverImage { get; set; }
        public byte[] ProfileImage { get; set; }

        public string IdbackImageName { get; set; }
        public string AreaName { get; set; }
        public string LocationName { get; set; }
        public string InChargePerson { get; set; }
        public string InchargePersonMobileNo { get; set; }
        public string NTNNumber { get; set; }
        public string InchargePersonEmail { get; set; }
        public bool? IsAllGoodStatus { get; set; }

        //WHEARHOUSER DETAIL
        public string WhearHousePersonName { get; set; }
        public string WhearHousePersonCountryName { get; set; }
        public string WhearHousePersonStateName { get; set; }
        public string WhearHousePersonAreaName { get; set; }
        public string WhearHousePersonLocationName { get; set; }
        public string WhearHousePersonMobileNumber { get; set; }
        public string WhearHouseAddress { get; set; }
        public string WhearHouseEmail { get; set; }
      


        //Return address DETAIL
        public string ReturnPersonName { get; set; }
        public string ReturnPersonCountryName { get; set; }
        public string ReturnPersonStateName { get; set; }
        public string ReturnPersonAreaName { get; set; }
        public string ReturnPersonLocationName { get; set; }
        public string ReturnHousePersonMobileNumber { get; set; }
        public string ReturnAddress { get; set; }
        public string ReturnEmailAddress { get; set; }
   

        //Bank  DETAIL
        public string AccountTitle { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string BranchCode { get; set; }
        public string IBAN { get; set; }
        public string ChequeImageName { get; set; }
        public byte[] ChequeImage { get; set; }


        public string FacebookUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string LinkedInUrl { get; set; }
        public string YoutubeUrl { get; set; }

        public string TwitterUrl { get; set; }
        public bool? IsActive { get; set; }

    }
}
