using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierModels.DTOs.ShippingApiDTO
{
    public class OrderTrackingDTO
    {
        public long order_id { get; set; }
        public int type { get; set; }
        public string UserRole { get; set; }
    }
}
