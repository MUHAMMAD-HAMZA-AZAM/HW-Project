using System;
using System.Collections.Generic;

namespace HW.SupplierViewModels
{
    public class PostAdVM
    {

        public long SupplierAdId { get; set; }
        public long SupplierId { get; set; }
        public long ProductCategoryId { get; set; }
        public long ProductSubcategoryId { get; set; }
        public long ProductSubCategory { get; set; }
        public string PostTitle { get; set; }
        public string PostDiscription { get; set; }
        public decimal Price { get; set; }
        public string Town { get; set; }
        public string Discount { get; set; }
        public string Address { get; set; }
        public long CityId { get; set; }
        public long StatusId { get; set; }
        public List<ImageVM> ImageVMs { get; set; }
        public VideoVM VideoVM { get; set; }
        public bool DeliveryAvailable { get; set; }
        public bool CollectionAvailable { get; set; }
        public int SupplierStatusId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime ActiveTo { get; set; }
        public string CreatedBy { get; set; }
        public string VideoPath { get; set; }
    }
}



