using System;
using System.Collections.Generic;

namespace HW.SupplierModels
{
    public partial class ProductVariant
    {
        public int Id { get; set; }
        public string ColorName { get; set; }
        public string HexCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
