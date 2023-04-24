using System;
using System.Collections.Generic;

namespace HW.SupplierModels
{
    public partial class SupplierSubCategory
    {
        public long ProductSetId { get; set; }
        public long SupplierId { get; set; }
        public long ProductSubCategoryId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
