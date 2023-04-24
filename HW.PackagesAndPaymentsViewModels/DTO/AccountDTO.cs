using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsViewModels.DTO
{
    // 3rd level account
    public class AccountDTO
    {
        public AccountDTO()
        {
            SubAccount = new List<SubAccountDTO>();
        }
        public long AccountId { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public int? AccountTypeId { get; set; }
        public bool? Active { get; set; }
        public int? AccountCategoryId { get; set; }
        public int? AccountSubCategoryId { get; set; }
        public bool? IsControlAccount { get; set; }
        public string EntityStatus { get; set; }
        public List<SubAccountDTO> SubAccount { get; set; }
    }
}
