using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsViewModels
{
    public class GetChartOfAccountVM
    {
        public string UserName { get; set; }
        public string AccountName { get; set; }
        public long LeadgerTransectionId { get; set; }
        public int? AccountId { get; set; }
        public long? UserId { get; set; }
        public long? JobQuotationId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string DocumentType { get; set; }
        public decimal? Debit { get; set; }
        public decimal? Credit { get; set; }
        public decimal? Amount { get; set; }
    }
}
