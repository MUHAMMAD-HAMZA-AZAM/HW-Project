using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsViewModels
{
   public class VoucherTypeVM
    {
        public int VoucherTypeId { get; set; }
        public int VoucherCategoryId { get; set; }
        public string VoucherCategoryName { get; set; }
        public string VoucherTypeCode { get; set; }
        public string VoucherTypeName { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
