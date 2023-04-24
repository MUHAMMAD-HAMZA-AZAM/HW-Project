using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
    public class SupplierProductDTO
    {
        public long? SupplierId { get; set; }
        public string Title { get; set; }
        public int NoOfRecords { get; set; }
        public string Slug { get; set; }
        public long ProductId { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public int? DiscountInPercentage { get; set; }
        public bool? Availability { get; set; }
        public string FileName { get; set; }
        public string ProductCategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string CategoryGroupName { get; set; }
        public string Name { get; set; }
        public string SupplierName { get; set; }
        public long? CategoryId { get; set; }
        public long? SubCategoryId { get; set; }
        public long? CategoryGroupId { get; set; }

        public string FilePath { get; set; }
        public long FileId { get; set; }
        public string SeoDescription { get; set; }
        public string SeoTitle { get; set; }
        public string OgTitle { get; set; }
        public string OgDescription { get; set; }
        public string Canonical { get; set; }

    }
}
