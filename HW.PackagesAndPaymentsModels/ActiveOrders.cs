using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsModels
{
    public partial class ActiveOrders
    {
        public long OrderId { get; set; }
        public string PackageName { get; set; }
        public long TotalAds { get; set; }
        public DateTime? ExpiryDate { get; set; }

    }
}
