using System;
using System.Collections.Generic;

namespace HW.SupplierModels
{
    public partial class ProductCategory
    {
        public long ProductCategoryId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public bool? HasSubItems { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public int? OrderByColumn { get; set; }
        public byte[] ProductImage { get; set; }
        public string SeoDescription { get; set; }
        public string Slug { get; set; }
        public string SeoTitle { get; set; }
        public string OgTitle { get; set; }
        public string OgDescription { get; set; }
        public string Canonical { get; set; }
    }
}
