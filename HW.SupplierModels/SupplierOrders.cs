using System;
using System.Collections.Generic;

namespace HW.SupplierModels
{
    public partial class SupplierOrders
    {
        public long Id { get; set; }
        public long SupplierId { get; set; }
        public long OrderId { get; set; }
        public int? OrderStatus { get; set; }
        public string TrackingId { get; set; }
    }
}
