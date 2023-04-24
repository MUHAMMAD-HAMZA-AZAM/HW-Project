using System;
using System.Collections.Generic;

#nullable disable

namespace HW.PackagesAndPaymentsModels
{
    public partial class VoucherBookPage
    {
        public int VoucherbookPageId { get; set; }
        public int? VoucherBookId { get; set; }
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
