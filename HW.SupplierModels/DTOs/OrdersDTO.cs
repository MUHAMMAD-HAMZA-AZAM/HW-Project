using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierModels.DTOs
{
    public class OrdersDTO
    {
        public long OrderId { get; set; }
        public long? SupplierId { get; set; }
        public long? CustomerId { get; set; }
        public string OrderFrom { get; set; }
        public string FirebaseClientId { get; set; }
        public string MobileNumber { get; set; }
        public string ShippingAddress { get; set; }
        public int OrderStatus { get; set; }
        public string OrderStatusName { get; set; }
        public string OrderMessage { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal? Price { get; set; }
        public decimal? TotalShippingAmount { get; set; }
        public decimal? ActualAmount { get; set; }
        public decimal? ShippingCost { get; set; }
        public decimal? Discount { get; set; }
        public decimal? PromotionAmount { get; set; }
        public decimal? TotalPayable { get; set; }
        public int? Quantity { get; set; }
        public string TrackingNumber { get; set; }
        public bool CancellationPolicy { get; set; }
        public string Comments { get; set; }
        public int? ReasonId { get; set; }
        public string CreatedBy { get; set; }
        public int? UserRole { get; set; }
        public decimal? Commission { get; set; }
    }
}
