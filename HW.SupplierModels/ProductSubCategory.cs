using System;
using System.Collections.Generic;

namespace HW.SupplierModels
{
    public partial class ProductSubCategory
    {
        public long ProductSubCategoryId { get; set; }
        public long ProductCategoryId { get; set; }
        public string SubCategoryCode { get; set; }
        public string SubCategoryName { get; set; }
        public bool IsActive { get; set; }
        public bool? HasSubItems { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? OrderByColumn { get; set; }
        public byte[] SubProductImage { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string OgTitle { get; set; }
        public string OgDescription { get; set; }
        public string Canonical { get; set; }
        public string Slug { get; set; }
    }
}
