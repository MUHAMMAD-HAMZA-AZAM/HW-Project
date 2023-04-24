using System;
using System.Collections.Generic;

#nullable disable

namespace HW.SupplierModels
{
    public partial class OrderItem
    {
        public long Id { get; set; }
        public long? SupplierId { get; set; }
        public long? ProductId { get; set; }
        public int? PromotionId { get; set; }
        public long? OrderId { get; set; }
        public long? VariantId { get; set; }
        public string TrackingNumber { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? DiscountedAmount { get; set; }
        public decimal? PromotionAmount { get; set; }
        public decimal? ShippingAmount { get; set; }
        public decimal? Discount { get; set; }
    }
}
