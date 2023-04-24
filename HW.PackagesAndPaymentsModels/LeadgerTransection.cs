using System;
using System.Collections.Generic;

namespace HW.PackagesAndPaymentsModels
{
    public partial class LeadgerTransection
    {
        public long LeadgerTransectionId { get; set; }
        public long? AccountId { get; set; }
        public long? SubAccountId { get; set; }
        public decimal? Debit { get; set; }
        public decimal? Credit { get; set; }
        public bool? Active { get; set; }
        public long? RefCustomerSubAccountId { get; set; }
        public long? RefTradesmanSubAccountId { get; set; }
        public long? RefSupplierSubAccountId { get; set; }
        public long? ReffrenceDocumentNo { get; set; }
        public long? ReffrenceDocumentId { get; set; }
        public string ReffrenceDocumentType { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string Description { get; set; }
        public int? RefDocumentLineId { get; set; }
        public int? FiscalPeriodId { get; set; }
        public DateTime? TransactionDate { get; set; }
    }
}
