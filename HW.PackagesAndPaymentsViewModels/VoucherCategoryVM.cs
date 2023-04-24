﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsViewModels
{
   public class VoucherCategoryVM
    {
        public int VoucherCategoryId { get; set; }
        public string VoucherCategoryCode { get; set; }
        public string VoucherCategoryName { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
