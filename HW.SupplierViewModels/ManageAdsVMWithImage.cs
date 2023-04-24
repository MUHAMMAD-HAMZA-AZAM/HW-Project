using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
   public class ManageAdsVMWithImage
    {
        public long SupplierAdsId { get; set; }
        public string AdTitle { get; set; }
        public decimal Price { get; set; }
        public int AdsStatusId { get; set; }
        public string SubCategoryValue { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime ActiveTo { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public string Addres { get; set; }
        public Byte[] AdImage { get; set; }
        public bool? IsActive { get; set; }
        public int? AdViewCount { get; set; }
        public long ProductCategoryId { get; set; }
    }
}
