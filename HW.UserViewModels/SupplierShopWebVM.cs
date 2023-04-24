using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
    public class SupplierShopWebVM
    {
        public long SupplierId { get; set; }
        public string SupplierCompanyName { get; set; }
        public string SupplierAddress { get; set; }
        public string LatLong { get; set; }
        public string ContactNo { get; set; }
        public byte[] SupplierImage { get; set; }
        public string Email { get; set; }
        public List<ShopSupplierAdVM> SupplierAds { get; set; }
        public List<SuppliersFeedbackVM> SupplierFeedbacks { get; set; }
    }
}
