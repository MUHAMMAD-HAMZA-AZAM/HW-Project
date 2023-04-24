using System;
using System.Collections.Generic;

#nullable disable

namespace HW.SupplierModels
{
    public partial class SupplierProductTag
    {
        public long TagId { get; set; }
        public long? ProductId { get; set; }
        public string TagName { get; set; }
    }
}
