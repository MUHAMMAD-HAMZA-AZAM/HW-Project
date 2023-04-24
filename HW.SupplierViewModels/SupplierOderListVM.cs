using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
  public  class SupplierOderListVM
    {

        public long? OrderId { get; set; }
        public string TrackingNumber { get; set; }
        public long? SupplierId { get; set; }
        public long? CustomerId { get; set; }
        public int? OrderStatusId { get; set; }
        public string OrderStatusName { get; set; }
        public String CustomerName { get; set; }
       public string CityName { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool PaymentReceivedStatus { get; set; }
        public int? SupplierPaymentStatus { get; set; }
        public decimal? Commission { get; set; }
        public decimal? TotalShippingCost { get; set; }
        public decimal? TotalPayableToSupplier { get; set; }
        public string CustomerFirebaseClientId { get; set; }
    }
}
