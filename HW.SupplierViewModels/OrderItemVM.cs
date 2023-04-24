using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
    public class OrderItemVM
    {
        public long Id { get; set; }
        public long? ProductId { get; set; }
        public long? SupplierId { get; set; }
        public string FirebaseClientId { get; set; }
        public long? CustomerId { get; set; }
        public int? PromotionId { get; set; }
        public decimal? PromotionAmount { get; set; }
        public long? OrderId { get; set; }
        public long? VariantId { get; set; }
        public int? Quantity { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public decimal? Price { get; set; }
        public decimal? Discount { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public decimal? ShippingAmount { get; set; }
        public bool? IsFreeShipping { get; set; }
    }
}
