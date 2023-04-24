using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierModels.DTOs
{
   public class FreeShippingDTO
    {
        public long? Id { get; set; }
        public long SupplierId { get; set; }
        public long? CategoryId { get; set; }
        public long? SubCategoryId { get; set; }
        public long? CategoryGroupId { get; set; }
        public DateTime? StarDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? Active { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
