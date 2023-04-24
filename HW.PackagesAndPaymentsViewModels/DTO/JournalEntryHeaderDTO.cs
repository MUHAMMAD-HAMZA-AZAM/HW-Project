using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsViewModels.DTO
{
    public class JournalEntryHeaderDTO
    {
        public int? id { get; set; }
        public DateTime? date { get; set; }
        public string referenceNo { get; set; }
        public string narration { get; set; }
        public string notes { get; set; }
        public string userId { get; set; }
        
    }
}
