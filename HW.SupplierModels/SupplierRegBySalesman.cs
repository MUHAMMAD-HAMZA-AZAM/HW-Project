using System;
using System.Collections.Generic;

namespace HW.SupplierModels
{
    public partial class SupplierRegBySalesman
    {
        public long Id { get; set; }
        public long? CustomerId { get; set; }
        public long? SalesmanId { get; set; }
    }
}
