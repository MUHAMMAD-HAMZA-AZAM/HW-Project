using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierModels.DTOs.ShippingApiDTO
{
    public class OrderItemTrackingDTO
    {
        public long tracking_number { get; set; }
        public int type { get; set; }
        public string UserRole { get; set; }
    }
}
