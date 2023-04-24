using System;
using System.Collections.Generic;

namespace HW.SupplierModels
{
    public partial class ProductAttribute
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
    }
}
