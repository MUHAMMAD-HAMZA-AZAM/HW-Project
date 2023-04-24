using System;
using System.Collections.Generic;

namespace HW.PackagesAndPaymentsModels
{
    public partial class SubAccount
    {
        public long SubAccountId { get; set; }
        public long? AccountId { get; set; }
        public string SubAccountNo { get; set; }
        public string SubAccountName { get; set; }
        public long? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public long? TradesmanId { get; set; }
        public string TradesmanName { get; set; }
        public long? SupplierId { get; set; }
        public string SupplierName { get; set; }
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
