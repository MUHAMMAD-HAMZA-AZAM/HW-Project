using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierModels.DTOs.ShippingApiDTO
{
   public class ResponseDTO
    {
        public long id { get; set; }
        public long status { get; set; }
        public string message { get; set; }
    }
}
