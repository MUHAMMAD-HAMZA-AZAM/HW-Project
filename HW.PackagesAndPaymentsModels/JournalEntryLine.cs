using System;
using System.Collections.Generic;

namespace HW.PackagesAndPaymentsModels
{
    public partial class JournalEntryLine
    {
        public int JournalEntryLineId { get; set; }
        public int JournalEntryHeaderId { get; set; }
        public long? SubAccountId { get; set; }
        public string Description { get; set; }
        public decimal? Debit { get; set; }
        public decimal? Credit { get; set; }
        public decimal? TaxDebit { get; set; }
        public decimal? TaxCredit { get; set; }
        public string EntityStatus { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
