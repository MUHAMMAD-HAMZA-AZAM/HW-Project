using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
    public class ProductInventoryVM
    {
        public long? Id { get; set; }
        public long? ProductId { get; set; }
        public int? Quantity { get; set; }
        public int? DiscountInPercentage { get; set; }
        public decimal? Price { get; set; }
        public int? ProductVariantId { get; set; }
        public bool? Availability { get; set; }
    }
}
