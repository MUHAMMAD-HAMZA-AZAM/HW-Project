using System;
using System.Collections.Generic;

namespace HW.SupplierModels
{
    public partial class ProductCatalogAttribute
    {
        public long Id { get; set; }
        public long? ProductId { get; set; }
        public int? AttributeId { get; set; }
        public string AttributeValue { get; set; }
    }
}
