using System;
using System.Collections.Generic;

namespace HW.PackagesAndPaymentsModels
{
    public partial class AccountCategory
    {
        public int AccountCategoryId { get; set; }
        public string AccountCategoryCode { get; set; }
        public string AccountCategoryName { get; set; }
        public bool? IsActive { get; set; }
        public string EntityStatus { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
