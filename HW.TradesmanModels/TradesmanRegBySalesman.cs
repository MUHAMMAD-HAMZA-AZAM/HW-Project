using System;
using System.Collections.Generic;

#nullable disable

namespace HW.TradesmanModels
{
    public partial class TradesmanRegBySalesman
    {
        public long Id { get; set; }
        public long? TradesmanId { get; set; }
        public long? SalesmanId { get; set; }
    }
}
