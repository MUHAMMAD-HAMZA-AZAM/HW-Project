using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
    public class SpSupplierListVM
    {
        public string Id { get; set; }
        public long SupplierId { get; set; }
        public bool isActive { get; set; }
        public string CompanyName { get; set; }
        public string CNIC { get; set; }
        public string UserId { get; set; }
        public bool IsTestUser { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string SupplierCategory { get; set; }
        public string SourceOfReg { get; set; }
        public string MobileNo { get; set; }
        public string SupplierAddress { get; set; }
        public string Email { get; set; }
        public string SalesmanName { get; set; }
        public int SupplierAdsCount { get; set; }
        public long NoOfRecoards { get; set; }
        public long RecordNo { get; set; }
        public long CountSupplierProduct { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastActive { get; set; }
        public byte[] ProfileImage { get; set; }
        public bool FeaturedSupplier { get; set; }
        public bool IsAllGoodStatus { get; set; }
    }
}
