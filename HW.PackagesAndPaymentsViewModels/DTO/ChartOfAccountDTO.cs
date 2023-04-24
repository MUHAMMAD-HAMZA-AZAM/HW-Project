using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsViewModels.DTO
{
    public class ChartOfAccountDTO
    {
        public ChartOfAccountDTO()
        {
            AccountCategory = new List<AccountCategoryDTO>();
        }
        public List<AccountCategoryDTO> AccountCategory { get; set; }
    }
}

