using System;
using System.Collections.Generic;

namespace HW.PackagesAndPaymentsModels
{
    public partial class Account
    {
        public long AccountId { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public int? AccountTypeId { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public int? AccountCategoryId { get; set; }
        public int? AccountSubCategoryId { get; set; }
        public bool? IsControlAccount { get; set; }
        public string EntityStatus { get; set; }
    }
}
