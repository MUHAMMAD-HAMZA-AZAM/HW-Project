using System;
using System.Collections.Generic;

namespace HW.SupplierModels
{
    public partial class ProductInventory
    {
        public long Id { get; set; }
        public long? ProductId { get; set; }
        public int? Quantity { get; set; }
        public int? DiscountInPercentage { get; set; }
        public decimal? Price { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public int? ProductVariantId { get; set; }
        public bool? Availability { get; set; }
        public decimal? Discount { get; set; }
    }
}
