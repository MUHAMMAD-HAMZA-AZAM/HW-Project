using System;
using System.Collections.Generic;

namespace HW.SupplierModels
{
    public partial class OrderItems
    {
        public long Id { get; set; }
        public long? SupplierId { get; set; }
        public long? ProductId { get; set; }
        public int? PromotionId { get; set; }
        public long? OrderId { get; set; }
        public long? VariantId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? DiscountedAmount { get; set; }
        public decimal? PromotionAmount { get; set; }
        public decimal? ShippingAmount { get; set; }
        public decimal? Discount { get; set; }
        public bool? IsFreeShipping { get; set; }
    }
}
