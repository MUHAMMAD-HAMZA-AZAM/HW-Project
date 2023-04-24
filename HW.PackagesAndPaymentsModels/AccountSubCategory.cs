using System;
using System.Collections.Generic;

namespace HW.PackagesAndPaymentsModels
{
    public partial class AccountSubCategory
    {
        public int AccountSubCategoryId { get; set; }
        public int? AccountCategoryId { get; set; }
        public string AccountSubCategoryCode { get; set; }
        public string AccountSubCategoryName { get; set; }
        public bool? IsActive { get; set; }
        public string EntityStatus { get; set; }
        public bool? IsControlAccount { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
