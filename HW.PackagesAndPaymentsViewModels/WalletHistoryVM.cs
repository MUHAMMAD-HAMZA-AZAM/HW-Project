using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsViewModels
{
    public class WalletHistoryVM
    {
        public long TransactionId { get; set; }
        public decimal? Debit { get; set; }
        public decimal? Credit { get; set; }
        public decimal? Total { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ReffrenceDocumentType { get; set; }
    }
}
