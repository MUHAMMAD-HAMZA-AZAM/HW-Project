using System;
using System.Collections.Generic;

namespace HW.SupplierModels
{
    public partial class ProductCatalog
    {
        public long ProductId { get; set; }
        public long? SupplierId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string YoutubeURL { get; set; }
        public long? CategoryId { get; set; }
        public long? SubCategoryId { get; set; }
        public long? CategoryGroupId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
        public bool? AuthorizedProduct { get; set; }
        public string ModifiedBy { get; set; }
        public decimal? Weight { get; set; }
    }
    public partial class ProductNames
    {
        public string Title { get; set; }
        
    }
}
