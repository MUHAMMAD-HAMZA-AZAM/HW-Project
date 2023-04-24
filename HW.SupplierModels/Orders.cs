using System;
using System.Collections.Generic;

namespace HW.SupplierModels
{
    public partial class Orders
    {
        public long Id { get; set; }
        public long? CustomerId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? TotalShippingCost { get; set; }
        public string OrderMessage { get; set; }
    }
}
