using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
   public class RatingsVM
    {
        public long SupplierId { get; set; }
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int? Ratings { get; set; }
        public DateTime CraetedOn { get; set; }


    }
}
