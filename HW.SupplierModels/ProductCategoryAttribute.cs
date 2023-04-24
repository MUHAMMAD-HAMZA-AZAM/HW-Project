using System;
using System.Collections.Generic;

namespace HW.SupplierModels
{
    public partial class ProductCategoryAttribute
    {
        public long Id { get; set; }
        public int? AttributeId { get; set; }
        public long? CategoryId { get; set; }
        public long? SubCategoryId { get; set; }
        public long? CategoryGroupId { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
