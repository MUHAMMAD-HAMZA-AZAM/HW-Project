using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
    public class ProductAttributesVM
    {
        public long? Id { get; set; }
        public long? ProductId { get; set; }
        public int? AttributeId { get; set; }
        public string AttributeValue { get; set; }
    }
}
