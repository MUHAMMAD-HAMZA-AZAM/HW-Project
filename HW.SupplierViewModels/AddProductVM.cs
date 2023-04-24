using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
    public class AddProductVM
    {
        public long Id { get; set; }
        public long? SupplierId { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public int noOfTopCategory { get; set; }
        public string ProductAttribute { get; set; }
        public string Description { get; set; }
        public string YoutubeURL { get; set; }
        public long? CategoryId { get; set; }
        public long? SubCategoryId { get; set; }
        public decimal? Weight { get; set; }
        public long? SubCategoryGroupId { get; set; }
        public bool? Active { get; set; }
        public string Action { get; set; }
        public int[] FileIdList{ get; set; }
        public List<ProductInventoryVM> ProductSku { get; set; }
        public List<BulkOrderingVM> BulkSku { get; set; }
        public List<ProductAttributesVM> ProductAttributes{ get; set; }
        public List<FileVM> Files{ get; set; }
        public List<ProductSearchTagVM> searchTag{ get; set; }
    }
}
