using System;
using System.Collections.Generic;

namespace HW.SupplierModels
{
    public partial class SupplierProductCategory
    {
        public long SupplierProductCategoryId { get; set; }
        public long ProductCategoryId { get; set; }
       
        public long SupplierId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
