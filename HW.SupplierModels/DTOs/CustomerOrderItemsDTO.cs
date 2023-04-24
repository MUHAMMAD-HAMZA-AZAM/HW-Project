using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierModels.DTOs
{
    public class CustomerOrderItemsDTO
    {
     
        public long? OrderId { get; set; }
        public string OrderFrom { get; set; }
        public int? Quantity { get; set; }
        public int? OrderStatus { get; set; }
        public string TrackingNumber { get; set; }
        public string OrderStatusName { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? noOfRecords { get; set; }
        public long? SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierEmailAddress { get; set; }
        public string SupplierMobileNumber { get; set; }
        public string CompanyName { get; set; }
        public long? ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public decimal? ActualPrice { get; set; }
        public decimal? DiscountedAmount { get; set; }
        public decimal? PromotionAmount { get; set; }
        public decimal? PayableAmount { get; set; }
        public decimal? ShippingCost { get; set; }
        public string Slug { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public bool? IsMain { get; set; }
        public bool? IsPaymentReceived { get; set; }
        public string VarientColour { get; set; }
        public long? VariantId { get; set; }
        public long? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ContactNumber { get; set; }
        public string ShippingAddress { get; set; }
        public string Comments { get; set; }
        public string ReasonName { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string CityName { get; set; }
        public bool IsPaymentModeConfirm { get; set; }
    }
}
