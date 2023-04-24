using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsViewModels.DTO
{
    public class AccountDetailData
    {
        public AccountDetailData()
        {
            AccountCategory = new List<AccountCategoryDTO>();
            AccountSubCategory = new List<AccountSubCategoryDTO>();
            Accounts = new List<AccountDTO>();
            SubAccounts = new List<SubAccountDTO>();
        }
        public List<AccountCategoryDTO> AccountCategory { get; set; }
        public List<AccountSubCategoryDTO> AccountSubCategory { get; set; }
        public List<AccountDTO> Accounts { get; set; }
        public List<SubAccountDTO> SubAccounts { get; set; }
    }
}
