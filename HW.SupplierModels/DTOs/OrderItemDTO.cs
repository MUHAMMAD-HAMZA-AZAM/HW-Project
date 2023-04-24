using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierModels.DTOs
{
    public class OrderItemDTO
    {
        public long OrderId { get; set; }
        public long SupplierId { get; set; }
        public long ProductId { get; set; }
        public string ProductTitle { get; set; }
        public long VariantId { get; set; }
        public string Variant { get; set; }
        public string TrackingNumber { get; set; }
        public string OrderFrom { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? TotalShippingAmount { get; set; }
        public decimal? ActualAmount { get; set; }
        public decimal? ShippingCost { get; set; }
        public decimal? TotalPayable { get; set; }
        public decimal? DiscountedAmount { get; set; }
        public decimal? Commission { get; set; }
        public decimal? PromotionAmount { get; set; }
    }
}
