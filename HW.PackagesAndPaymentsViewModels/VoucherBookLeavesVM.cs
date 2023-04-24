using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsViewModels
{
   public class VoucherBookLeavesVM
    {
        public int VoucherBookLeavesId { get; set; }
        public int VoucherBookId { get; set; }
        public string VoucherBookName { get; set; }
        public int VoucherTypeId { get; set; }
        public string VoucherTypeName { get; set; }
        public int? PageNumber { get; set; }
        public string VoucherNo { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public bool? IsUsed { get; set; }
        public decimal? DiscountedAmount { get; set; }
        public decimal? PersentageDiscount { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
