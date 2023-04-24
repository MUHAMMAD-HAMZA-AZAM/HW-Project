using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierModels.DTOs
{
   public class GetFreeShippingListDTO
    {
        public long FreeShippingId { get; set; }
        public long CategoryId { get; set; }
        public long? SubCategoryId { get; set; }
        public long? CategoryGroupId { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string CategoryGroupName { get; set; }
        public DateTime StarDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Status { get; set; }
    }
}
