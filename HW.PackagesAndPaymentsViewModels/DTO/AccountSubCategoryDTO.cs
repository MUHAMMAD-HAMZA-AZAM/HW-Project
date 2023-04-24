using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsViewModels.DTO
{
    // 2nd level account
    public class AccountSubCategoryDTO
    {
        public AccountSubCategoryDTO()
        {
            Account = new List<AccountDTO>();
        }
        public int AccountSubCategoryId { get; set; }
        public int? AccountCategoryId { get; set; }
        public string AccountSubCategoryCode { get; set; }
        public string AccountSubCategoryName { get; set; }
        public bool? IsActive { get; set; }
        public string EntityStatus { get; set; }
        public bool? IsControlAccount { get; set; }
        public List<AccountDTO> Account { get; set; }
    }
}
