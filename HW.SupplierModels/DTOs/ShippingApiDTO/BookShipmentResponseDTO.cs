using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierModels.DTOs.ShippingApiDTO
{
   public class BookShipmentResponseDTO
    {
        public string status { get; set; }
        public string message { get; set; }
        public string tracking_number { get; set; }
    }
}
