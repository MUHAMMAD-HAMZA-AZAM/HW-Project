using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
    public class ProductVM
    {
        public long Id { get; set; }
        public long SupplierId { get; set; }
        public string FirebaseClientId { get; set; }
        public int? Quantity { get; set; }
        public int PageNumber { get; set; }
        public int NoOfRecords { get; set; }
        public int PageSize { get; set; }
        public decimal? Price { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public int DiscountInPercentage { get; set; }
        public decimal? ToPrice { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public string YoutubeURL { get; set; }
        public int? VariantId { get; set; }
        public long? BulkVarientId { get; set; }
        public long? FileId { get; set; }
        public string VariantName { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public bool? IsMain { get; set; }
        public string ShopName { get; set; }
        public string ShopUrl { get; set; }
        public bool? Availability { get; set; }
        public string HexCode { get; set; }
        public int? AttributeId { get; set; }
        public string AttributeValue { get; set; }
        public string AttributeName { get; set; }
        public string Action { get; set; }
        public string CategoryLevel { get; set; }
        public long? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public long? SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public long? CategoryGroupId { get; set; }
        public string CategoryGroupName { get; set; }
        public bool? Active { get; set; }
        public string Slug { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public long? bulkId { get; set; }
        public decimal? bulkdiscount { get; set; }
        public int? minQuantity { get; set; }
        public int? maxQuantity { get; set; }
        public decimal? bulkPrice { get; set; }
        //public long? bulkVarientId { get; set; }
        public long? bulkProductId { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Weight { get; set; }
        public string Display { get; set; }
        public long? Value { get; set; }
        public long? TagId { get; set; }
        public int? count { get; set; }
        public long? TraxCityId { get; set; }
        public string TagName { get; set; }
        public bool? IsFreeShipping { get; set; }

        public double? AverageRating { get; set; }
    }
}
