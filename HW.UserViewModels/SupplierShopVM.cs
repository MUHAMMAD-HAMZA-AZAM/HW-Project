using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
   public class SupplierShopVM
    {
        public long SupplierId { get; set; }
        public string SupplierCompanyName { get; set; }
        public string SupplierAddress { get; set; }
        public string SupplierCity { get; set; }

        public List<ShopSupplierAdVM> SupplierAds { get; set; }

    }

    public class ShopSupplierAdVM
    {
        public long AdId { get; set; }
        public string AdTitle { get; set; }
        public long CategoryId { get; set; }
        public Decimal Price { get; set; }
        public byte[] AdImage { get; set; }
        public string ImageName { get; set; }
        public string SupplierCompanyName { get; set; }
        public long AdImageId { get; set; }

    }

}
