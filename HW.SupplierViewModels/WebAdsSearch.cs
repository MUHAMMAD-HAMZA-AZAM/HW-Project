using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
    public class WebAdsSearch
    {
        public long SupplierAdId { get; set; }
        public string SupplierAdTitle { get; set; }
        public decimal Price { get; set; }
        public int AdsStatusId { get; set; }
        public string SubCategoryValue { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime ActiveTo { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public string Addres { get; set; }
        public string AdImageName { get; set; }
        public bool? IsActive { get; set; }
        public int? AdViewCount { get; set; }
        public byte[] SupplierAdImage { get; set; }
        public string SupplierCompanyName { get; set; }
    }
}
