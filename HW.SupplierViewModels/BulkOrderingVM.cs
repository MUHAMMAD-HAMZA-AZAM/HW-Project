using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
    public class BulkOrderingVM
    {
        public int minQuantity { get; set; }
        public int maxQuantity { get; set; }
        public decimal bulkPrice { get; set; }
        public decimal? bulkDiscount { get; set; }
        public long? varientId { get; set; }
        public long? bulkVarientId { get; set; }
    }
}
