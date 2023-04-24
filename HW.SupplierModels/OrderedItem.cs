using System;
using System.Collections.Generic;

namespace HW.SupplierModels
{
    public partial class OrderedItem
    {
        public long Id { get; set; }
        public long? Productid { get; set; }
        public int? PromotionId { get; set; }
        public long? OrderId { get; set; }
        public long? VariantId { get; set; }
        public int? Quantity { get; set; }
    }
}
