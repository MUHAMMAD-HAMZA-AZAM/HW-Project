using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsViewModels.DTO
{
    // 4th level account
    public class SubAccountDTO
    {
        public long SubAccountId { get; set; }
        public long? AccountId { get; set; }
        public string SubAccountNo { get; set; }
        public string SubAccountName { get; set; }
        public bool? Active { get; set; }
        public int? AccountCategoryId { get; set; }
        public int? AccountSubCategoryId { get; set; }
        public bool? IsControlAccount { get; set; }
        public string EntityStatus { get; set; }
    }
}
