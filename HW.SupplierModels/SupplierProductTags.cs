using System;
using System.Collections.Generic;

namespace HW.SupplierModels
{
    public partial class SupplierProductTags
    {
        public long TagId { get; set; }
        public long? ProductId { get; set; }
        public string TagName { get; set; }
    }
}
