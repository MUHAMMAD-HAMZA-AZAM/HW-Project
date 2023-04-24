using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsViewModels.DTO
{
    public class DetailedGLReportDTO
    {
        public long LeadgerTransectionId { get; set; }
        public long AccountId { get; set; }
        public long SubAccountId { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public long ReferenceNo { get; set; }
        public string Description { get; set; }
        public DateTime? TransactionDate { get; set; }
        public decimal OpeningBalance { get; set; }
        public string SubAccountName { get; set; }
        public string ReffrenceDocumentType { get; set; }
    }
}
