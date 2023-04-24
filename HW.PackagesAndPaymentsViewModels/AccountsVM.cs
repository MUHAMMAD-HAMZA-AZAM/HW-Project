using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsViewModels
{
    public class AccountsVM
    {

        public int? pageSize { get; set; }
        public int? pageNumber { get; set; }
        public long id { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string Action { get; set; }

        public string AccountTypeCode { get; set; }
        public string AccounTypeName { get; set; }
        public bool? Active { get; set; }
        //account
        public long AccountId { get; set; }
        public long? UserId { get; set; }
        public string UserName { get; set; }
        public int? AccountNo { get; set; }
        public string AccountName { get; set; }
        public long AccountTypeId { get; set; }

        //subaccount
        //public long SubAccountId { get; set; }

        public string SubAccountNo { get; set; }
        public string SubAccountName { get; set; }
        public long? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public long? TradesmanId { get; set; }
        public string TradesmanName { get; set; }
        public long? SupplierId { get; set; }
        public string SupplierName { get; set; }

        public int noOfRecoards { get; set; }

        //Common
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
