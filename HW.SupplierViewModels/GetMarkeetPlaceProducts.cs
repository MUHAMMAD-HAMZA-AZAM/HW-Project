using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
   public class GetMarkeetPlaceProducts     
    {
        public long SupplierAdId { get; set; }
        public string SupplierAdTitle { get; set; }
        public string SupplierCompanyName { get; set; }
        public decimal Price { get; set; }
        public byte [] ImageContent { get; set; }
        public long AdImageId { get; set; }
        public int IsSaved { get; set; }
        public int IsLiked { get; set; }
        public int AdStatus { get; set; }
        public int AdRating { get; set; }
        public long ProductCategoryId { get; set; }
        public int TotalProducts { get; set; }
    }
}
