using System;
using System.Collections.Generic;

namespace HW.SupplierModels
{
    public partial class SupplierWithProductList
    {
        public long SupplierWithProductListId { get; set; }
        public long SupplierId { get; set; }
        public long SupplierProductCategoryId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
