using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsViewModels.DTO
{
    // 1st level account
    public class AccountCategoryDTO
    {
        public AccountCategoryDTO()
        {
            AccountSubCategory = new List<AccountSubCategoryDTO>();
        }
        public int AccountCategoryId { get; set; }
        public string AccountCategoryCode { get; set; }
        public string AccountCategoryName { get; set; }
        public bool? IsActive { get; set; }
        public string EntityStatus { get; set; }
        public List<AccountSubCategoryDTO> AccountSubCategory { get; set; }
    }
}
