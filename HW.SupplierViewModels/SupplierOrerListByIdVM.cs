using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
   public class SupplierOrerListByIdVM
    {

        public long? ProductId { get; set; }
        public string ProductName { get; set; }
        public long? SupplierId { get; set; }
        public long? OrderDetailId { get; set; }
        public long? VariantId { get; set; }
        public long? OrderId { get; set; }
        public long? ItemId { get; set; }
        public long? CustomerId { get; set; }
        public string CustomerFirebaseClientId { get; set; }
        public string SuplierName { get; set; }
        public string CustomerShippingAddress { get; set; }
        public string ShippingCity { get; set; }
        public string CustomerName { get; set; }
        public string TrackingNumber { get; set; }
        public string CustomerAddress { get; set; }
        public string MobileNumber { get; set; }
        public decimal ShippingAmount { get; set; }
        public bool PaymentReceivedStatus	 { get; set; }
        public bool DispatchPaymentStatus	 { get; set; }
        public string ColorName { get; set; }
        public string OrderStatus { get; set; }
        public int? OrderStatusId { get; set; }
        public int? Quantity { get; set; }
        public decimal? ProductPrice { get; set; }
        public decimal? Total { get; set; }
        public decimal? ProductDiscountAmount { get; set; }
        public decimal? ProductPromotionAmount { get; set; }
        public decimal? TotalPayable { get; set; }

        public DateTime? CreatedOn { get; set; }
    }
}
