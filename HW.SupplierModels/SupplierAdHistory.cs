using System;
using System.Collections.Generic;

#nullable disable

namespace HW.SupplierModels
{
    public partial class SupplierAdHistory
    {
        public string UserId { get; set; }
        public string AddDetails { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
