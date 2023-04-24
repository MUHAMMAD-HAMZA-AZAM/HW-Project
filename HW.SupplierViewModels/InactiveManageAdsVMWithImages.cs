using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
   public class InactiveManageAdsVMWithImages
    {
        public long SupplierAdsId { get; set; }
        public string AdTitle { get; set; }
        public decimal Price { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime ActiveTo { get; set; }
        public string City { get; set; }
        public string SubCategoryValue { get; set; }
        public string Town { get; set; }
        public string Addres { get; set; }
        public string FileName { get; set; }
        public Byte[] AdImage { get; set; }
        public Double TotalDay { get; set; }
       
    }
}
