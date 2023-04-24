using System;
using System.Collections.Generic;

namespace HW.SupplierModels
{
    public partial class BulkOrdering
    {
        public long Id { get; set; }
        public long? SupplierId { get; set; }
        public long? ProductId { get; set; }
        public int? MinQuantity { get; set; }
        public int? MaxQuantity { get; set; }
        public decimal? BulkPrice { get; set; }
        public decimal? Discount { get; set; }
        public long? BulkVarientId { get; set; }

    }
}
