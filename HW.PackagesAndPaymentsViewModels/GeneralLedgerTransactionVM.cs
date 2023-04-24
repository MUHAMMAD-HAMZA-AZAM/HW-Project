using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsViewModels
{
    public class GeneralLedgerTransactionVM
    {
        public long LeadgerTransectionId { get; set; }
        public long? AccountId { get; set; }
        public long? SubAccountId { get; set; }
        public decimal? Credit { get; set; }
        public decimal? Debit { get; set; }
        public decimal? Balance { get; set; }
        public bool Active { get; set; }
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
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public long? userId { get; set; }
        public string Audit { get; set; }
        public decimal? TotalCreditBalance { get; set; }
        public decimal? TotalDebitBalance { get; set; }
        public string Discription { get; set; }
        public DateTime? TransectionDate { get; set; }
    }
}
