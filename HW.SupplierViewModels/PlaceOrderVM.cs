using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
    public class PlaceOrderVM
    {
        public long OrderItemId { get; set; }
        public bool isFromWeb { get; set; }
        public long? SupplierId { get; set; }
        public long? CustomerId { get; set; }
        public decimal? OrderTotal { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? OrderStatus { get; set; }
        public decimal? TotalShippingCost { get; set; }
        public string OrderMessage { get; set; }
        public List<OrderItemVM> items { get; set; }
        public List<OrderItemVM> supplierIdArr { get; set; }
        public ShippingDetailsVM shippingDetails { get; set; }
    }
}
