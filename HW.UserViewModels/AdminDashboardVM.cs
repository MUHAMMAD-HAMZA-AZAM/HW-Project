using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
   public class AdminDashboardVM
    {
        public long AllUsersCount { get; set; }

        public long CreatedJobsCount { get; set; }
        public long AuthorizeJobsCount { get; set; }
        public long RegisteredCustomersCount { get; set; }
        public long LiveAdsCount { get; set; }
        public long SupplierCounts { get; set; }
        public long TradesmanCount { get; set; }
        public long TotalCountLhr { get; set; }
        public long TotalCountIsb { get; set; }
        public long TotalCountKhi { get; set; }
        public long TotalCountGujrat { get; set; }
        public long TotalCountGujWala { get; set; }
        public string City { get; set; }
        public string SkillName { get; set; }
        public long TradesmanCountBySkill { get; set; }
        public long CustomerCount { get; set; }
        public string CustomerSkillName { get; set; }
        public string CustomerCity { get; set; }
        public long SupplieCount { get; set; }
        public string SupplierCategory { get; set; }
        public string SupplierCity { get; set; }
        public string OrgCity { get; set; }
        public string OrgSkillName { get; set; }
        public long OrgCount { get; set; }
        //////////////////////////
        public long SpLhr { get; set; }
        public long TrdLhr { get; set; }
        public long CsLhr { get; set; }
        public long SpIsb { get; set; }
        public long TrdIsb { get; set; }
        public long CsIsb { get; set; }
        public long SpKhi { get; set; }
        public long TrdKhi { get; set; }
        public long CsKhi { get; set; }
        public long SpGuj { get; set; }
        public long TrdGuj { get; set; }
        public long CsGuj { get; set; }
        public long SpGwa { get; set; }
        public long TrdGwa { get; set; }
        public long CsGwa { get; set; }

    }
}
