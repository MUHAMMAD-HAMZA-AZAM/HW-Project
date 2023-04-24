using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsViewModels
{
   public class VoucherBookPagesVM
    {
        public int VoucherbookPageId { get; set; }
        public int? VoucherBookId { get; set; }
        public string VoucherBookName { get; set; }
        public int? NoOfPages { get; set; }
        public int? NoOfLeavesPerPage { get; set; }
        public int? PageNumber { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
