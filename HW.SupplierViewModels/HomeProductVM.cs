using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
   public class HomeProductVM
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public byte[] ProductImage { get; set; }
        public string ImageName { get; set; }
    }
}
