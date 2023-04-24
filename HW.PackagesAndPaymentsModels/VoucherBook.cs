using System;
using System.Collections.Generic;

namespace HW.PackagesAndPaymentsModels
{
    public partial class VoucherBook
    {
        public int VoucherBookId { get; set; }
        public int VoucherTypeId { get; set; }
        public string VoucherBookNo { get; set; }
        public string VoucherBookName { get; set; }
        public bool? IsAssigned { get; set; }
        public int? NoOfLeaves { get; set; }
        public int? NoOfPages { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public decimal? BookLevelAmountDiscount { get; set; }
        public double? BookLevelPersentageDiscount { get; set; }
        public int NoOfTotalLeaves { get; set; }
    }
}
