using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{

    public class ProductCategory_HomeVM
    {
        public List<SupplierAdVM> SupplierAd { get; set; }
        public List<CategoryListVM> CategoryList { get; set; }
    }

        public class SupplierAdVM
    {
        public long SupplierAdId { get; set; }
        public string SupplierAdTitle { get; set; }
        public string SupplierCompanyName { get; set; }
        public decimal Price { get; set; }
        public byte[] SupplierAdImage { get; set; }
        public long AdImageId { get; set; }
        public string ImageName { get; set; }
        public bool ImageLoaded { get; set; }
        public bool IsSaved { get; set; }
        public int AdStatus { get; set; }
        public long CustomerSavedAdsId { get; set; }
        public long SubCategoryId { get; set; }
    }

    public class CategoryListVM
    {
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }

    }
}
